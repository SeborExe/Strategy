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

        HandleROtation();

        HandleZoom();
    }

    private void HandleMove()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x += 1f;
        }

        float moveSpeed = 10f;

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleROtation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y += 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y -= 1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            followTargetOffset.y -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            followTargetOffset.y += zoomAmount;
        }

        float zoomSpeed = 5f;
        followTargetOffset.y = Mathf.Clamp(followTargetOffset.y, minFollowOffset, maxFollowOffset);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(
            cinemachineTransposer.m_FollowOffset, followTargetOffset, Time.deltaTime * zoomSpeed);
    }
}
