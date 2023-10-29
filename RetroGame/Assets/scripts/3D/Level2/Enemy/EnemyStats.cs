using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class EnemyStats : MonoBehaviour
    {
        public float life;
        public float viewDistance;
        public Transform player;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        protected bool playerIsInView;
        protected bool playerIsInRange;
        public float speed = 2f; // Velocidade de movimento do inimigo
        protected float timeSinceLastSeenPlayer = 0f;
        public float fireRate = 2;
        protected float fireTimer;
        protected bool isWalking = false;
        public Animator anim;
        public CharacterController characterController;

        private void LateUpdate()
        {
            // Aumente o fireTimer a cada quadro
            fireTimer += Time.deltaTime;

            if (!playerIsInView)
            {
                timeSinceLastSeenPlayer += Time.deltaTime;
            }
            else
            {
                timeSinceLastSeenPlayer = 0f;
            }
            fieldOfView();
        }


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
            GameObject.Destroy(gameObject);
        }

        public virtual void fieldOfView()
        {
            playerIsInView = false;
            playerIsInRange = false;
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                // Verifique se há obstáculos entre o inimigo e o jogador
                if (Physics.Raycast(transform.position, dirToTarget, out RaycastHit hit, viewDistance))
                {
                    if (!((obstacleMask.value & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)) {
                    // Verifique se o jogador está dentro do alcance do SphereCollider
                        playerIsInView = true;
                        if (Vector3.Distance(transform.position, target.position) > (GetComponent<SphereCollider>().radius / 2 - 5))
                        {
                            playerIsInRange = false;
                            // O jogador está fora do alcance, então mova o inimigo em direção ao jogador
                            MoveTowardsPlayer(target);
                            if (!isWalking)
                            {
                                anim.SetBool("walk", true);
                                isWalking = true;
                            }
                        }
                        else
                        {
                            playerIsInRange = true;
                            // O jogador está dentro do alcance, então ataque
                            if (fireTimer > fireRate)
                            {
                                Attack();
                                fireTimer = 0;
                            }
                            if (isWalking)
                            {
                                anim.SetBool("walk", false);
                                isWalking = false;
                            }

                        }
                    }
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
            //Código para atacar
        }

        public virtual void MoveTowardsPlayer(Transform target)
        {
            // Obtenha a direção para o jogador
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // Mova o inimigo em direção ao jogador
            characterController.SimpleMove(dirToTarget * speed);
        }
    }
}