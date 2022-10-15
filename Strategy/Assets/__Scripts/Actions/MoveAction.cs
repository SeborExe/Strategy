using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private float stoppingDistance = 0.1f;
    private List<Vector3> positionList;
    private int currentPositionIndex;

    [SerializeField] int maxMoveDistance = 4;
    [SerializeField] string actionName = "Move";
    [SerializeField] int actionPointsCost = 2;

    public event Action OnStartMoving;    
    public event Action OnStopMoving;    

    private void Update()
    {
        if (!isActive) return;

        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke();
                ActionComplete();
            }
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        List<GridPosition> pathGridPositionList =  Pathfinding.Instance.FindPath(unit.GetGridPosition(), gridPosition,
            out int pathLength);

        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        }

        OnStartMoving?.Invoke();
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z < maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (unitGridPosition == testGridPosition) continue;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;
                if (!Pathfinding.Instance.HasPath(unitGridPosition, testGridPosition)) continue;

                int pathfindingDistanceMultiplier = 10;
                if (Pathfinding.Instance.GetPathLenght(unitGridPosition, testGridPosition) >
                    maxMoveDistance * pathfindingDistanceMultiplier) continue; 

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return actionName;
    }

    public override int GetActionPointsCost()
    {
        return actionPointsCost;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10
        };
    }
}
