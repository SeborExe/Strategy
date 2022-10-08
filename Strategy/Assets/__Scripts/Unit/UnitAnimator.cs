using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] Transform bulletProjectilePrefab;
    [SerializeField] Transform shootPointTransform;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void MoveAction_OnStartMoving()
    {
        animator.SetBool("IsWalking", true);
    }

    private void MoveAction_OnStopMoving()
    {
        animator.SetBool("IsWalking", false);
    }

    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");

        Transform bulletProjectileTrans = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTrans.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootPosition = e.targetUnit.GetWorldPosition();
        targetUnitShootPosition.y = shootPointTransform.position.y;

        bulletProjectile.SetUp(targetUnitShootPosition);
    }
}
