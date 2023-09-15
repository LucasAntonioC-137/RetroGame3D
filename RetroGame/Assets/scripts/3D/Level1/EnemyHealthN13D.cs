using RailShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealthN13D : MonoBehaviour
{
    public int health = 100;
    public bool destroyedByPlayer = false;
    public int score = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            destroyedByPlayer = true;
            PlayerStats.instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}
