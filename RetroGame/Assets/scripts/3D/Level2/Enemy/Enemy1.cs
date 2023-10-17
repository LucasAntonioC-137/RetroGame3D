using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace level2
{
    public class Enemy1 : EnemyStats
    {
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float fireRate = 2;
        private float fireTimer;
        public Animator anim;
        private bool isAttacking = false;
        private float timeSinceLastSeenPlayer = 0f;

        void Start()
        {
            life = 100f; // Defina a vida inicial aqui
        }

        private void Update()
        {
            // Aumente o fireTimer a cada quadro
            fireTimer += Time.deltaTime;

            // Se o fireTimer for maior que o fireRate, chame Fire() e redefina o fireTimer
            if (fireTimer > fireRate)
            {
                Attack();
                fireTimer = 0;
            }
            fieldOfView();
            if (!playerIsInView)
            {
                timeSinceLastSeenPlayer += Time.deltaTime;
            }
            else
            {
                timeSinceLastSeenPlayer = 0f;
            }
        }


        public void Walk()
        {
            // Código para o inimigo andar
        }

        public override void Attack()
        {
            // Verifique se o jogador está dentro do campo de visão antes de atirar
            if (playerIsInView)
            {
                if (!isAttacking)
                {
                    anim.SetBool("attack", true);
                    Debug.Log("Atirando");
                    // Instancie a bala na posição do ponto de disparo
                    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    isAttacking = true;
                }
            }
            else
            {
                if (isAttacking && timeSinceLastSeenPlayer>=2) 
                {
                    anim.SetBool("attack", false);
                    isAttacking = false;
                }
            }
        }

    }
}
