using Level2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionAnimation; // arraste o prefab da anima��o de explos�o para este campo no inspetor
    public float explosionRadius = 2.5f; // Raio da explos�o
    public int explosionDamage = 100; // Dano da explos�o

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullets")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Instancie a anima��o de explos�o na posi��o do barril
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

        // Aqui voc� pode adicionar o c�digo para criar o efeito visual da explos�o
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
