using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level2
{
    public class EnemyStats : MonoBehaviour
    {
        public float life;
        public float viewDistance;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        protected bool playerIsInView;

        public void GetDamage(float damage)
        {
            life -= damage;
            if (life <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            // C�digo para a morte do inimigo
        }

        public virtual void fieldOfView()
        {
            playerIsInView = false;
            // Obtenha o raio do SphereCollider
            //float viewDistance = GetComponent<SphereCollider>().radius;

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                // Verifique se h� obst�culos entre o inimigo e o jogador
                if (!Physics.Raycast(transform.position, dirToTarget, viewDistance, obstacleMask))
                {
                    // O jogador est� dentro do campo de vis�o e n�o h� obst�culos entre eles
                    Attack();
                    playerIsInView= true;
                }
            }
        }
        void OnDrawGizmos()
        {
            // Desenhe uma linha vermelha no editor da Unity para representar o Raycast
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);
        }


        public virtual void Attack()
        {
            //C�digo para atacar
        }
    }
}