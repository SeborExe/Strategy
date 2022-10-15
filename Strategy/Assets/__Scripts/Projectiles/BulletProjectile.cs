using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 200f;

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] Transform bulletHitVFX;

    public void SetUp(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);  

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);

            Instantiate(bulletHitVFX, targetPosition, Quaternion.identity);
        }
    }
}
