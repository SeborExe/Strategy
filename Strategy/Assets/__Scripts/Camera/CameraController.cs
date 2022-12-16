using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private const float minFollowOffset = 2f;
    private const float maxFollowOffset = 12f;

    private Vector3 followTargetOffset;
    private CinemachineTransposer cinemachineTransposer;

    private void Start()
    {
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        followTargetOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMove();

        HandleRotation();

        HandleZoom();
    }

    private void HandleMove()
    {
        Vector2 inputMoveDirection = InputManager.Instance.GetCameraMoveVector();

        float moveSpeed = 10f;

        Vector3 moveVector = transform.forward * inputMoveDirection.y + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        rotationVector.y = InputManager.Instance.GetCameraRotationAmount();

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomIncreaseAmount = 1f;
        followTargetOffset.y += InputManager.Instance.GetCameraZoomAmount() * zoomIncreaseAmount;

        float zoomSpeed = 5f;
        followTargetOffset.y = Mathf.Clamp(followTargetOffset.y, minFollowOffset, maxFollowOffset);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(
            cinemachineTransposer.m_FollowOffset, followTargetOffset, Time.deltaTime * zoomSpeed);
    }
}
