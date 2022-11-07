using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;//시작 체력
    public float health { get; protected set; }//현재 체력

    protected virtual void OnEnable()
    {
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        health -= damage;
    }
}
