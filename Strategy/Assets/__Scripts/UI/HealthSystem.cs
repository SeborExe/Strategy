using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;


    public event EventHandler OnDead;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}
