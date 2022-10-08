using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int healthMax = 100;
    private int health;

    public event EventHandler OnDead;
    public event EventHandler OnDamage;

    private void Awake()
    {
        health = healthMax;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnDamage?.Invoke(this, EventArgs.Empty);

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

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
}
