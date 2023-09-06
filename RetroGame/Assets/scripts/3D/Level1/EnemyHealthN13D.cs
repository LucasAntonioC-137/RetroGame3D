using RailShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthN13D : MonoBehaviour
{
    public int health = 100;
    public bool destroyedByPlayer = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            destroyedByPlayer = true;
            Destroy(gameObject);
        }
    }
}
