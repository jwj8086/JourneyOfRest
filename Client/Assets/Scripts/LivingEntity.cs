using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;//���� ü��
    public float health { get; protected set; }//���� ü��

    protected virtual void OnEnable()
    {
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        health -= damage;
    }
}
