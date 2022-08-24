using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity, IDamageable
{
    public virtual void EnemyOnDamage(float damage) { return; } // 사용안함
    public Slider healthSlider;

    private PlayerMovement3 playerMovement;
    private PlayerShooter playerShooter;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement3>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        startingHealth = 5;
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);

        healthSlider.value = newHealth;
    }
    // 나중에 최대 체력 증가도 만들어야함.

    public virtual void PlayerOnDamage()
    {
        health--;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
        
    //}
}
