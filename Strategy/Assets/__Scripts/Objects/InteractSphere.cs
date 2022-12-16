using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] Material greenMaterial;
    [SerializeField] Material redMaterial;
    [SerializeField] MeshRenderer meshRenderer;

    private float timer;
    private Action onInteractComplete;
    private bool isActive;
    private GridPosition gridPosition;

    private bool isGreen;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        SetColorRed();
    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            onInteractComplete();
        }
    }

    private void SetColorGreen()
    {
        isGreen = true;
        meshRenderer.material = greenMaterial;
    }

    private void SetColorRed()
    {
        isGreen = false;
        meshRenderer.material = redMaterial;
    }

    public void Interact(Action onInteractionComplete)
    {
        this.onInteractComplete = onInteractionComplete;
        isActive = true;
        timer = 0.5f;

        if (isGreen)
        {
            SetColorRed();
        }
        else
        {
            SetColorGreen();
        }
    }
}
