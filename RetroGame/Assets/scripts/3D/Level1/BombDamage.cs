using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BombDamage : MonoBehaviour
{
    public int damage = 100;
    private new ParticleSystem particleSystem;
    private SphereCollider sphereCollider;
    private float initialParticleSize;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();
        initialParticleSize = particleSystem.main.startSize.constant;
    }

    void Update()
    {
        if (particleSystem != null && sphereCollider != null)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
            particleSystem.GetParticles(particles);

            float currentSize = particles[0].GetCurrentSize(particleSystem);
            sphereCollider.radius = currentSize / 2f - 1;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        EnemyHealthN13D enemyHealth = other.GetComponent<EnemyHealthN13D>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

}
