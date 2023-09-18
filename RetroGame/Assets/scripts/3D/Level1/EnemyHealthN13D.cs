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
    public AudioSource explosionSfx;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(explosionSfx.clip, transform.position);
            //explosionSfx.Play();
            destroyedByPlayer = true;
            
            PlayerStats.instance.AddScore(score);
            Destroy(gameObject);
            if (gameObject.GetComponent<DestroyVFX>() != null)
            {
                GameObject prefabVfx = gameObject.GetComponent<DestroyVFX>().prefabVfx;
                GameObject vfx = Instantiate(prefabVfx, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }
        }
    }
}
