#define USE_NEW_INPUT_SYSTEM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton error in: " + transform);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMouseWorldPosition()
    {
#if USE_NEW_INPUT_SYSTEM
        return Mouse.current.position.ReadValue();
#else
        return Input.mousePosition;
#endif
    }

    public bool IsMouseButtonDownThisFrame()
    {
#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.MouseClick.WasPressedThisFrame();
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    public Vector2 GetCameraMoveVector()
    {
#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.CameraMovement.ReadValue<Vector2>();
#else
        Vector2 inputMoveDirection = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.y += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.y -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x += 1f;
        }

        return inputMoveDirection;
#endif
    }

    public float GetCameraRotationAmount()
    {

#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.CameraRotate.ReadValue<float>();
#else
        float rotationAmount = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationAmount = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationAmount = -1f;
        }

        return rotationAmount;
#endif
    }

    public float GetCameraZoomAmount()
    {
#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.CameraZoom.ReadValue<float>();
#else
        float zoomAmount = 0f;

        if (Input.mouseScrollDelta.y > 0)
        {
            zoomAmount = -1f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoomAmount = +1f;
        }

        return zoomAmount;
#endif
    }
}
