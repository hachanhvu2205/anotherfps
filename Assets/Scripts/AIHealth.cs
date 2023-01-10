using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool alive = true;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void GotDamage(float amount)
    {
        healthBar.SetHealth(currentHealth);
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
           
            Debug.Log("Fall Death");
        }
    }

    void Die()
    {
        healthBar.SetHealth(0);
        GetComponent<Animator>().SetBool("Idle",true);
        // GetComponent<EnemyMovement>().alive = false;
        // GetComponent<Animator>().SetTrigger("Die");
        GetComponent<Animator>().SetTrigger("Die");
        alive = false;
        // GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject,2f);
    }

}
