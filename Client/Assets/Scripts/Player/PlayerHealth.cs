using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;

    private Movement2D Movement2D;
    private Weaponary Weaponary;

    private void Awake()
    {
        Movement2D = GetComponent<Movement2D>();
        Weaponary = GetComponent<Weaponary>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        Movement2D.enabled = true;
        Weaponary.enabled = true;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = health;
    }
}
