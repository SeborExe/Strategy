using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCamera;

    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;
    }

    private void BaseAction_OnAnyActionStarted(object sender, System.EventArgs e)
    {
        switch(sender)
        {
            case ShootAction shootAction:
                SetActionCameraPosition(shootAction);
                ShowActionCamera();
                break;
        }
    }

    private void SetActionCameraPosition(ShootAction shootAction)
    {
        Unit shooterUnit = shootAction.GetUnit();
        Unit targetUnit = shootAction.GetTargetUnit();

        Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
        Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

        float shoulderOffsetAmount = 0.5f;
        Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;

        Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + (shootDir * -1);

        actionCamera.transform.position = actionCameraPosition;
        actionCamera.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
    }

    private void BaseAction_OnAnyActionCompleted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }

    private void ShowActionCamera()
    {
        actionCamera.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCamera.SetActive(false);
    }
}
