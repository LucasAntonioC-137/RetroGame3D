using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Level3
{
    public class BlackBomb : MonoBehaviour
    {
        public List<Transform> points;
        public float speed = 2.0f;
        public float chaseSpeed = 4.0f;
        public float rotationSpeed = 10.0f;
        public GameObject player;
        private int destPoint = 0;
        private bool chasingPlayer = false;
        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
            GotoNextPoint();
        }

        void GotoNextPoint()
        {
            if (points.Count == 0)
                return;

            chasingPlayer = false;
            destPoint = (destPoint + 1) % points.Count;
        }

        void Update()
        {
            Walk();
        }

        void Walk()
        {
            float step = speed * Time.deltaTime;
            Vector3 targetDir = Vector3.zero;

            RaycastHit hit;
            Vector3 rayDirection = transform.forward;

            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (hit.transform == player.transform && Vector3.Distance(player.transform.position, transform.position) < 2)
                {
                    chasingPlayer = true;
                    targetDir = player.transform.position - transform.position;
                    step = chaseSpeed * Time.deltaTime;
                }
            }
            else if (!chasingPlayer && Vector3.Distance(transform.position, points[destPoint].position) < 0.5f)
            {
                GotoNextPoint();
            }

            if (!chasingPlayer)
            {
                targetDir = points[destPoint].position - transform.position;
            }

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, chasingPlayer ? player.transform.position : points[destPoint].position, step);
            
            //Setting walk or run animations
            if (chasingPlayer)
            {
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 2);
            }
                

            // Desenhar o raio continuamente
            Debug.DrawRay(transform.position, rayDirection * 2, Color.red);
        }
    }
}