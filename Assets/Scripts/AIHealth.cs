using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        healthBar.SetHealth(currentHealth);
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            Debug.Log("Fall Death");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
