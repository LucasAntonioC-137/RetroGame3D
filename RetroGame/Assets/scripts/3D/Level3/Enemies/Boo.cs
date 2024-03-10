using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;

namespace Level3
{
    public class Boo : MonoBehaviour
    {
        [Header("Status")]
        public float life = 6;

        [Header("Status")]
        public float speed = 4.0f;
        public float chaseSpeed = 8.0f;
        public float rotationSpeed = 10.0f;

        [Header("Agro")]
        public GameObject target;
        public bool canSeePlayer;

        [Header("Path")]
        public List<Transform> pathPoints = new List<Transform>();
        public int currentPathIndex = 0;

        private bool chasingPlayer = false;
        private Vector3 moveDirection;
        private NavMeshAgent agent;
        private bool playerIsDead;

        [Header("Floating")]
        public float floatingSpeed = 1f; // Adjust speed as needed
        public float floatingAmplitude = 0.5f; // Adjust floating height variation
        public float floatingFrequency = 2f; // Adjust floating movement frequency

        private float timeOffset = 0f;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player;
            }
            agent = GetComponent<NavMeshAgent>();
            playerIsDead = target.GetComponent<PlayerControl>().isDead;
            canSeePlayer = gameObject.GetComponent<FieldOfView>().canSeePlayer;
        }

        void moveToNextPoint()
        {
            if (pathPoints.Count > 0)
            {
                float distance = Vector3.Distance(pathPoints[currentPathIndex].position, transform.position);
                agent.destination = pathPoints[currentPathIndex].position;

                if (distance <= 4f)
                {
                    currentPathIndex++;
                    currentPathIndex %= pathPoints.Count;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            Walk();
            //Floating();
        }

        void Floating()
        {
            // Calculate floating position offset
            float floatOffset = Mathf.Sin(Time.time * floatingFrequency + timeOffset) * floatingAmplitude;

            // Apply floating offset to transform position
            transform.position = new Vector3(transform.position.x, transform.position.y + floatOffset, transform.position.z);
        }

        public void GetHit()
        {

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
            canSeePlayer = gameObject.GetComponent<FieldOfView>().canSeePlayer;

            if (agent.enabled)
            {
                if (!playerIsDead)
                {
                    if (canSeePlayer)
                    {
                        agent.SetDestination(target.transform.position);
                        chasingPlayer = true;
                        agent.stoppingDistance = 7;

                        // Tornar o personagem transparente
                        Renderer renderer = GetComponent<Renderer>();
                        foreach (Material material in renderer.materials)
                        {
                            Color color = material.color;
                            color.a = 0.5f;  // Altere este valor para ajustar a transparência
                            material.color = color;
                        }
                    }
                    else
                    {
                        chasingPlayer = false;
                        moveToNextPoint();
                        agent.stoppingDistance = 6;

                        // Tornar o personagem opaco
                        Renderer renderer = GetComponent<Renderer>();
                        foreach (Material material in renderer.materials)
                        {
                            Color color = material.color;
                            color.a = 1f;
                            material.color = color;
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
                    LookTarget();
                }
                else if (!chasingPlayer)
                {
                    agent.acceleration = speed;
                    moveToNextPoint();
                }
            }
        }
    } 
}