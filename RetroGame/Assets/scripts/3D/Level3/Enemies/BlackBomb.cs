using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace Level3
{
    public class BlackBomb : MonoBehaviour
    {
        [Header("Status")]
        public float damage = 40;
        public float speed = 4.0f;
        public float chaseSpeed = 8.0f;
        public float rotationSpeed = 10.0f;

        [Header("Agro")]
        public GameObject target;
        public float lookDistance = 2f;

        [Header("Path")]
        public List<Transform> pathPoints = new List<Transform>();
        public int currentPathIndex = 0;

        private bool chasingPlayer = false;
        private Animator anim;
        private Vector3 moveDirection;
        private float chaseTime = 0f;
        private bool startCountdown = false;
        private NavMeshAgent agent;
        private bool playerIsDead;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerIsDead = target.GetComponent<PlayerControl>().isDead;
        }

        void moveToNextPoint()
        {
            if(pathPoints.Count > 0)
            {
                float distance = Vector3.Distance(pathPoints[currentPathIndex].position, transform.position);
                agent.destination = pathPoints[currentPathIndex].position;

                if(distance <= 4f)
                {
                    currentPathIndex++;
                    currentPathIndex %= pathPoints.Count;
                }
            }
        }

        void Update()
        {
            Walk();
        }

        void LookTarget()
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        void Walk()
        {
            playerIsDead = target.GetComponent<PlayerControl>().isDead;
            // Verifica se o jogador está dentro do alcance usando um raycast
            RaycastHit hit;
            Vector3 rayDirection = transform.forward;
            moveDirection.y -= 9.81f * Time.deltaTime; // Aplica a gravidade
            if (!playerIsDead)
            {
                if (Physics.Raycast(transform.position, rayDirection, out hit))
                {
                    if (hit.transform == target.transform && Vector3.Distance(target.transform.position, transform.position) < 2)
                    {
                        print(playerIsDead);
                        if (!chasingPlayer)
                        {
                            chasingPlayer = true;
                            startCountdown = true;
                        }

                    }
                }

            }
            else
            {
                chasingPlayer = false;
            }


            if (chasingPlayer)
            { 
                agent.acceleration = chaseSpeed;
                agent.SetDestination(target.transform.position);
                anim.SetInteger("transition", 2);
                LookTarget();
            }
            else if (!chasingPlayer)
            {
                agent.acceleration = speed;
                moveToNextPoint();
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
            }

            if (startCountdown)
            {
                chaseTime += Time.deltaTime;
                if (chaseTime >= 5f)
                {
                    // Causa dano em uma área esférica ao redor do inimigo
                    Explode();

                    // Destroi o inimigo
                    Destroy(gameObject);
                }
                else if(playerIsDead)
                {
                    startCountdown= false;
                }
            }


            Debug.DrawRay(transform.position, rayDirection * 2, Color.red);
        }

        void Explode()
        {
            // Define o raio da explosão
            float radius = 5f;

            // Obtém todos os objetos dentro do raio da explosão
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in objectsInRange)
            {
                // Verifica se o objeto é o jogador
                PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
                
                if (player != null)
                {
                    // Causa dano ao jogador
                    player.GetDamage(damage);
                }
            }
        }
    }
}