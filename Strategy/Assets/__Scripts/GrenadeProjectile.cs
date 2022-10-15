using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private Action OnGranadeBehaviourComplete;
    public static event EventHandler OnAnyGrenadeExploded;

    private Vector3 targetPosition;
    private float moveSpeed = 15f;
    private float reachTargetDistance = 0.2f;
    [SerializeField] private float radius = 4f;
    [SerializeField] private int damage = 30;
    [SerializeField] private Transform grenadeProjectileVFX;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private AnimationCurve arcYAnimationCurve;
    private float totalDistance;
    private Vector3 positionXZ;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - positionXZ).normalized;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistance;

        float maxHeight = totalDistance / 4f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);

        if (Vector3.Distance(positionXZ, targetPosition) < reachTargetDistance)
        {
            Collider[] colliderArray =  Physics.OverlapSphere(targetPosition, radius);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(damage);
                }
            }

            OnAnyGrenadeExploded?.Invoke(this, EventArgs.Empty);

            trail.transform.parent = null;
            Instantiate(grenadeProjectileVFX, targetPosition + Vector3.up * 1f, Quaternion.identity);

            Destroy(gameObject);
            OnGranadeBehaviourComplete();
        }
    }

    public void SetUp(GridPosition targetGridPosition, Action OnGranadeBehaviourComplete)
    {
        this.OnGranadeBehaviourComplete = OnGranadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);

        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistance = Vector3.Distance(positionXZ, targetPosition);
    }
}
