using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseWorldPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.groundLayerMask);
        return raycastHit.point;
    }
}
