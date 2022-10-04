using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;

    public event EventHandler OnSelectedUnitChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton error in: " + transform);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy) return;

        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            switch(selectedAction)
            {
                case MoveAction moveAction:
                    if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        selectedUnit.GetMoveAction().Move(mouseGridPosition, CleraBusy);
                    }
                    break;

                case SpinAction spinAction:
                    SetBusy();
                    selectedUnit.GetSpinAction().Spin(CleraBusy);
                    break;
            }
        }
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }

        return false;
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void CleraBusy()
    {
        isBusy = false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }     

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
