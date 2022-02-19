using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class AliveController : MonoBehaviour
{
    [Range(1, 1000)] public int maxHealth = 100;
    public DamageAcceptor acceptor = DamageAcceptor.Player;

    public virtual int Health
    {
        get => health;
        set
        {
            if (health > 0 && value <= 0)
            {
                Death();
            }

            health = value;
            HealthChanged?.Invoke(this, value);
        }
    }

    [SerializeField] protected int health;
    public UnityEvent<AliveController, int> HealthChanged;
    public UnityEvent<AliveController, int> MaxHealthChanged;
    public UnityEvent<AliveController> WasDead;

    protected void Start()
    {
        MaxHealthChanged?.Invoke(this, maxHealth);
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
        WasDead?.Invoke(this);

    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DamageZone damageZone))
        {
            if (acceptor == damageZone.acceptor && !damageZone.Zone)
                damageZone.GiveGamege(this);
        }

        if (other.gameObject.TryGetComponent(out GetHeal getHeal))
        {
            if (acceptor == getHeal.acceptor)
                this.GetHeal(getHeal.HealValue);
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DamageZone damageZone))
        {
            if (acceptor == damageZone.acceptor && damageZone.Zone)
                damageZone.GiveGamege(this);
        }

        if (other.gameObject.TryGetComponent(out GetHeal getHeal))
        {
            if (acceptor == getHeal.acceptor)
                this.GetHeal(getHeal.HealValue);
        }
    }

    private void EntetZone(Collider other)
    {

    }
}
