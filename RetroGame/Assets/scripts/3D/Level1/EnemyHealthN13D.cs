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
            // Instantiate the audio source prefab
            AudioSource laserSfxInstance = Instantiate(explosionSfx, transform.position, Quaternion.identity);
            // Play the sound
            laserSfxInstance.Play();
            // Destroy the audio source after the sound has finished playing
            Destroy(laserSfxInstance.gameObject, laserSfxInstance.clip.length);
            
            destroyedByPlayer = true;
            
            PlayerStats.instance.AddScore(score);
            
            if (gameObject.GetComponent<DestroyVFX>() != null)
            {
                GameObject prefabVfx = gameObject.GetComponent<DestroyVFX>().prefabVfx;
                GameObject vfx = Instantiate(prefabVfx, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }
            Destroy(gameObject);
        }
    }
}
