using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts.Player
{

    public abstract class AliveController : MonoBehaviour
    {
        public virtual int MaxHealth
        {
            get => _maxHealth;
            set
            {
                if (value < 0)
                    return;

                _maxHealth = value;
                MaxHealthChanged?.Invoke(this, value);
            }
        }
        public virtual int Health
        {
            get => _health;
            set
            {
                if (_health > 0 && value <= 0)
                {
                    Death();
                }

                _health = value;
                HealthChanged?.Invoke(this, value);
            }
        }
        public DamageAcceptor acceptor = DamageAcceptor.Player;

        [SerializeField, Range(1, 1000)] protected int _maxHealth = 100;
        [SerializeField] protected int _health;

        public AudioSource audioSource;
        public AudioClip getDamage;
        public AudioClip getHeal;
        public AudioClip death;

        public UnityEvent<AliveController, int> HealthChanged;
        public UnityEvent<AliveController, int> MaxHealthChanged;
        public UnityEvent<AliveController> WasDead;

        protected virtual void Start()
        {
            MaxHealthChanged?.Invoke(this, MaxHealth);
            Health = MaxHealth;
        }

        public virtual void GetDamage(int damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                if (audioSource != null)
                    audioSource.PlayOneShot(getDamage);
            }
        }
        public virtual void GetHeal(int heal)
        {
            if (Health < _maxHealth)
            {
                Health += heal;
                if (audioSource != null)
                    audioSource.PlayOneShot(getHeal);
            }
        }
        protected virtual void Death()
        {
            if (audioSource != null)
                audioSource.PlayOneShot(death);
            WasDead?.Invoke(this);
        }

        protected void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DamageZone damageZone))
            {
                if (acceptor == damageZone.acceptor && !damageZone.Zone)
                    damageZone.GiveDamage(this);
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
                    damageZone.GiveDamage(this);
            }

            if (other.gameObject.TryGetComponent(out GetHeal getHeal))
            {
                if (acceptor == getHeal.acceptor)
                    this.GetHeal(getHeal.HealValue);
            }
        }

    }
}