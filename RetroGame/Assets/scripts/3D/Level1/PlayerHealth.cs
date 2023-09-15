using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private CameraShake cameraShake;
    private PlayButtonScript menus;

    void Start()
    {
        currentHealth = maxHealth;
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        menus = GameObject.FindObjectOfType<PlayButtonScript>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        cameraShake.TriggerShake();

        if (currentHealth <= 0)
        {
            menus.PlayerDied();
        }
    }
}
