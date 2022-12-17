using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] Transform bulletProjectilePrefab;
    [SerializeField] Transform shootPointTransform;
    [SerializeField] Transform rifleTransform;
    [SerializeField] Transform swordTransform;

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

        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordAction_OnSwordActionCompleted;
        }
    }

    private void Start()
    {
        EquipRifle();
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

    private void SwordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
        EquipRifle();
    }

    private void SwordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        EquipSword();
        animator.SetTrigger("SwordSlash");
    }

    private void EquipSword()
    {
        swordTransform.gameObject.SetActive(true);
        rifleTransform.gameObject.SetActive(false);
    }

    private void EquipRifle()
    {
        swordTransform.gameObject.SetActive(false);
        rifleTransform.gameObject.SetActive(true);
    }
}
