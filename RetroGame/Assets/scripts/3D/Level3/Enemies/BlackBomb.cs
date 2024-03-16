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
        public float damage = 2;
        public float speed = 4.0f;
        public float chaseSpeed = 8.0f;
        public float rotationSpeed = 10.0f;
        public bool isDead = false;
        public float score = 20;

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
        //Specifics
        private bool captured = false;
        private bool thrown = false;
        private bool readyToThrow;

        void Start()
        {
            // Find the player object with the "Player" tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player;
            }
            else
            {
                Debug.LogError("Player object with tag 'Player' not found!");
            }
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerIsDead = target.GetComponent<PlayerControl>().isDead;
        }

        public bool Captured
        {
            get { return captured; }
            set { captured = value; }
        }
        
        public bool Thrown
        {
            get { return thrown; }
            set { thrown = value; }
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
                    // Check if it's the last path point AND the enemy is within a tolerance (optional)
                    if (currentPathIndex == 0 && distance <= 4f && pathPoints.Count > 8) // Adjust tolerance as needed
                    {
                        Explode();
                    }
                }

            }
        }

        void Update()
        {
            Walk();
            CapturedByPlayer();
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
            // Verifica se o jogador est� dentro do alcance usando um raycast
            RaycastHit hit;
            Vector3 rayDirection = transform.forward;
            if (agent.enabled)
            {

                moveDirection.y -= 9.81f * Time.deltaTime; // Aplica a gravidade
                if (!playerIsDead)
                {
                    if (Physics.Raycast(transform.position, rayDirection, out hit))
                    {
                        if (hit.transform == target.transform && Vector3.Distance(target.transform.position, transform.position) < 2)
                        {
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
            }

            if (startCountdown)
            {
                chaseTime += Time.deltaTime;
                if (chaseTime >= 5f)
                {
                    // Causa dano em uma �rea esf�rica ao redor do inimigo
                    Explode();
                }
                else if(playerIsDead)
                {
                    startCountdown= false;
                }
            }


            Debug.DrawRay(transform.position, rayDirection * 2, Color.red);
        }

        void CapturedByPlayer()
        {
            if (captured && !readyToThrow)
            {
                chaseTime = 0;
                readyToThrow= true;
                //agent.isStopped= true;
                startCountdown = true;
                anim.SetInteger("transition", 0);
                print("PEGARO");
            }else if (thrown && captured)
            {
                captured= false;
                chaseTime = 4f;
                print("JOGARO");
            }
        }

        void Explode()
        {
            // Define o raio da explos�o
            float radius = 5f;

            // Obt�m todos os objetos dentro do raio da explos�o
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in objectsInRange)
            {
                // Check for enemies more efficiently
                if (col.CompareTag("Enemy"))
                {
                    Destroy(col.gameObject);
                }
                // Verifica se o objeto � o jogador
                PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
                Boo bossBoo = col.gameObject.gameObject.GetComponent<Boo>();
                if (player != null)
                {
                    // Causa dano ao jogador
                    Vector3 damageDirection = player.transform.position - transform.position;
                    player.GetDamage(damage, damageDirection);
                }
                else if(bossBoo != null)
                {
                    Vector3 damageDirection = col.transform.position - transform.position;
                    bossBoo.GetHit(damageDirection);
                }
                if (thrown)
                {
                    player.playerScore += score;
                }
            }
            isDead = true;

            Destroy(gameObject);
        }
    }
}