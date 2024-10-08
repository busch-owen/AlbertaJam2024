using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : PoolObject
{
    [SerializeField] private float projectileLifetime = 1f;
    [SerializeField] public float _damage;
    [SerializeField] public float isEnemy;
    public Rigidbody2D RB { get; private set; }

    protected virtual void OnEnable()
    {
        RB = GetComponent<Rigidbody2D>();
        Invoke(nameof(OnDeSpawn), projectileLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke();
            OnDeSpawn();
            AudioManager.Instance.PlaySFX("sFuny");
        }
    }
}
