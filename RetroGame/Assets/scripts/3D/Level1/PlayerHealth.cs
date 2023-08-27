using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Passou");
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // O player morreu
            Debug.Log("Player morreu!");
        }
    }
}
