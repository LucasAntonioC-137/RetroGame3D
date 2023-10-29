using Level2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionAnimation; // arraste o prefab da animação de explosão para este campo no inspetor
    public float explosionRadius = 2.5f; // Raio da explosão
    public int explosionDamage = 100; // Dano da explosão

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullets")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Instancie a animação de explosão na posição do barril
        Explode();
        GameObject explosion = Instantiate(explosionAnimation, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }

    void Explode()
    {
        // Cria uma esfera ao redor do barril
        // Cria uma esfera ao redor do barril
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            // Verifica se o objeto atingido tem um componente EnemyStats
            Level2.EnemyStats enemy = hit.GetComponent<Level2.EnemyStats>();

            if (enemy != null)
            {
                // Se tiver, causa dano
                enemy.GetDamage(explosionDamage);
            }
            Level2.PlayerBase player = hit.GetComponent<Level2.PlayerBase>();

            if (player != null)
            {
                player.GetDamage(explosionDamage);
            }
        }

        // Aqui você pode adicionar o código para criar o efeito visual da explosão
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
