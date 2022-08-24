using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action onDeath;

    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if(dead)
        { return; }

        health += newHealth;
    }

    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath();
        }

        dead = true;
    }
}
