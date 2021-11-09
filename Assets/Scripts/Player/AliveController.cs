using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AliveController : MonoBehaviour
{
    [Range(1, 1000)] public int maxHealth = 100;

    public virtual int Health
    {
        get => health;
        set
        {
            health = value;
            HealthChanged?.Invoke(gameObject, value);
        }
    }

    [SerializeField] protected int health;
    public UnityEvent<GameObject, int> HealthChanged;
    public UnityEvent<GameObject> WasDead;

    protected void Start()
    {
        Health = maxHealth;
    }

    public virtual void GetDamage(int damage)
    {
        if (Health > 0)
            Health -= damage;
    }
    public virtual void GetHeal(int heal)
    {
        if (Health < maxHealth)
            Health += heal;
    }
    protected virtual void Death()
    {
        WasDead?.Invoke(gameObject);
        Debug.Log("You Dead");
    }
}
