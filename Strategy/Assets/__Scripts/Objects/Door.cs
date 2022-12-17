using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour, IInteractable
{
    Animator animator;
    GridPosition gridPosition;
    private float timer;
    private Action onInteractComplete;
    private bool isActive;

    [SerializeField] private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
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

    private void ChangeDoorState()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;

        ChangeDoorState();
    }

    private void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("IsOpen", true);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, true);
    }

    private void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("IsOpen", false);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }
}
