using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

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
        player.transform.position = respawnPoint.transform.position;
    }

}

