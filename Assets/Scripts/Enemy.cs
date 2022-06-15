using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Health
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }    

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeHeal(float healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
    }

    public void TakeDamage(float damageAmount)
    {
        if (!IsDead())
        {
            currentHealth = Mathf.Max(0, currentHealth - damageAmount);
            if (IsDead())
            {
                Die();
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= Mathf.Epsilon;
    }
}
