using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

namespace Level3
{
    public class PlayerControl : MonoBehaviour
    {
        private CharacterController characterController;
        private Animator anim;
        private Vector3 velocity;
        private float timeSinceJump;

        public Transform playerHand;

        public bool isDead = false;
        public float playerHealth = 100;
        public float speed = 2f;
        public float gravityValue = -9.81f;
        private float _velocity;
        private Vector3 _direction;
        public float rotationSpeed;
        private int jumpCount = 0;
        public bool isJumping;
        public bool isWalking = true;
        public bool withObject;
        public float verticalSpeed;
        public float magnitud;
        public float maxJumpTime = 0.5f; // O tempo máximo que o personagem pode passar no ar
        public float jumpForce = 10f; // A força máxima do salto
        public float maxTimeBetweenJumps = 2;
        public float doubleJumpForce = 15;
        public float knockbackStrength = 10;
        public bool knockBack = false;
        private BlackBomb bomb;
        private float originalSpeed;
        private float originalRotation;


        private Vector3 slideDirection;
        public float slopeLimit = 45f; // Ângulo máximo de inclinação que o personagem pode subir
        public float slideSpeed = 10f; // Velocidade de deslizamento
        private Vector3 normal;
        // Start is called before the first frame 


        void Start()
        {
            characterController= GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            anim.SetInteger("transition", 0);
            jumpCount= 0;
            originalSpeed = speed;
            originalRotation = rotationSpeed;
        }   

        // Update is called once per frame
        void Update()
        {
            if (!isDead && !knockBack)
            {
                characterController.transform.position = characterController.transform.position;
                ApplyGravity();
                Jump();
                Move();
                Take_Object();
            }
            if (characterController.isGrounded)
            {
                // Calcula a inclinação do terreno

                float angle = Vector3.Angle(normal, Vector3.up);

                // Se a inclinação for maior que o limite, aplica o deslizamento
                if (angle > slopeLimit)
                {
                    slideDirection = Vector3.ProjectOnPlane(normal, -Vector3.up);
                    slideDirection *= slideSpeed;

                    // Aplica a gravidade ao deslizamento
                    slideDirection.y += gravityValue * Time.deltaTime;

                    characterController.Move(slideDirection * Time.deltaTime);
                }
            }
            else if(isDead)
            {
                characterController.enabled= false;
                return;
            }
            if(bomb != null)
            {
                if (bomb.isDead)
                {
                    withObject = false;
                }
            }
            else
            {
                withObject= false;
            }
            
        }

        public void AddHealth()
        {
            if(playerHealth < 6)
            {
                playerHealth++;
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.normal != Vector3.up) // Check if not colliding with top surface
            {
                normal = hit.normal; // Access the normal from the collision
            }
        }

        public void GetDamage(float damage, Vector3 damageDirection)
        {
            playerHealth -= damage;
            if(playerHealth <= 0)
            {
                PlayerDie();
            }
            knockBack = true;
            if (!isDead)
                StartCoroutine(Knockback(0.5f, damageDirection));
        }
        public IEnumerator Knockback(float duration, Vector3 hitDirection)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                characterController.Move(hitDirection * knockbackStrength * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            knockBack = false;
        }


        void PlayerDie()
        {
            isDead = true;
            anim.SetTrigger("Die");
        }

        void Move()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
            { 
                // Calculate the forward and right directions based on the camera's rotation
                Vector3 forwardDirection = Camera.main.transform.forward;
                Vector3 rightDirection = Camera.main.transform.right;

                // Remove any vertical movement from the directions
                forwardDirection.y = 0;
                rightDirection.y = 0;
                forwardDirection.Normalize();
                rightDirection.Normalize();

                // Calculate the movement direction based on the input
                Vector3 movementDirection = (forwardDirection * verticalInput) + (rightDirection * horizontalInput);
                movementDirection.Normalize();
                isWalking = true;
                if (!isJumping || IsGrounded())
                {
                    if (!withObject)
                    {
                        anim.SetInteger("transition", 1);
                    }
                    else
                    {
                        anim.SetInteger("transition", 5);
                    }
                }

                // Move the character in the new direction
                float speedModifier = withObject ? 0.5f : 1f; // Reduce speed by 50% if withObject is true

                if (characterController.enabled)
                {
                    characterController.Move(movementDirection * Time.deltaTime * speed * speedModifier);

                }
                // If there is any movement, rotate the character to face the movement direction
                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }

            }
            else if (!isJumping && isWalking)
            {
                if (!withObject)
                    anim.SetInteger("transition", 0);
                else
                    anim.SetInteger("transition", 4);
            }
        }

        void Jump()
        {
            if (!isJumping)
            {
                rotationSpeed = originalRotation;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !withObject)
            {
                if (jumpCount == 0)
                {
                    // Aplica a força do pulo
                    _direction.y = jumpForce;
                    isJumping = true;
                    rotationSpeed = rotationSpeed / 2;
                    characterController.Move(_direction * Time.deltaTime);
                    anim.SetInteger("transition", 2);
                    jumpCount++;
                    timeSinceJump = 0; // Reseta o tempo desde o último pulo
                }
                else if (jumpCount == 1 && timeSinceJump < maxTimeBetweenJumps)
                {
                    _direction.y = doubleJumpForce;
                    isJumping = true;
                    rotationSpeed = rotationSpeed / 2;
                    anim.SetInteger("transition", 3);
                    jumpCount++;
                }
                isJumping = true;
                isWalking = false;
            }

            if (timeSinceJump > maxTimeBetweenJumps && characterController.isGrounded)
            {
                jumpCount = 0;
                timeSinceJump = 0;
            }
            if (jumpCount == 1)
            {
                timeSinceJump += Time.deltaTime;
            }

            // Check if the character has landed
            if (characterController.isGrounded)
            {
                //anim.SetInteger("transition", 1);
                isJumping = false;
                if (jumpCount == 2)
                {
                    jumpCount = 0;
                }
            }
            // Adiciona a velocidade ao movimento do personagem
            characterController.Move(_direction * Time.deltaTime);
        }

        private bool IsGrounded() => characterController.isGrounded;

        private void ApplyGravity()
        {
            if (IsGrounded())
            {
                _direction.y = -1.0f;
            }
            else
            {
                _direction.y += gravityValue * Time.deltaTime;
            }
        }

        void Take_Object()
        {
            float heightOffset = 0.5f; // Change this value as needed
            // Define the range within which the character can take an object
            float range = 2.0f;
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!withObject)
                {
                    Ray ray = new Ray(transform.position + new Vector3(0, heightOffset, 0), transform.forward);
                    
                    // Define the hit information
                    RaycastHit hit;

                    // Cast the ray
                    if (Physics.Raycast(ray, out hit, range))
                    {
                        // Check if the object is the one you want to take
                        if (hit.collider.gameObject.CompareTag("Catchable"))
                        {
                            withObject = true;
                            // Take the object (you can define what this means for your game)
                            Debug.Log("Object taken: " + hit.collider.gameObject.name);
                            Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                            if (hit.collider.gameObject.GetComponent<BlackBomb>())
                            {
                                bomb = hit.collider.gameObject.GetComponent<BlackBomb>();
                                bomb.Captured = true;

                            }
                            NavMeshAgent agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                            if (agent != null)
                            {
                                agent.enabled= false;
                            }
                            // Make the object a child of the character's hand
                            hit.collider.gameObject.transform.SetParent(playerHand);
                            anim.SetInteger("transition", 4);
                            // Position the object at the location of the character's hand
                            hit.collider.gameObject.transform.position = playerHand.position;

                            // Adjust the position of the object so that its bottom is at the transform location
                            float objectHeight = hit.collider.bounds.size.y;
                            hit.collider.gameObject.transform.position += new Vector3(0, objectHeight / 2, 0);
                            if (!hit.collider.gameObject.GetComponent<BlackBomb>())
                            {
                                float forwardOffset = 0.6f; // You can adjust this value as needed
                                hit.collider.gameObject.transform.position += playerHand.forward * forwardOffset;
                            }
                            // Reset the rotation of the object
                            hit.collider.gameObject.transform.localRotation = Quaternion.identity;

                            if (rb != null)
                            {
                                rb.isKinematic = true;
                            }

                        }
                    }
                }
                else
                {
                    print(playerHand.childCount);
                    if (playerHand.childCount > 0)
                    {
                        // Get the object that the character is holding
                        Transform objectInHand = playerHand.GetChild(0);

                        if (objectInHand.gameObject.GetComponent<BlackBomb>())
                        {
                            bomb = objectInHand.gameObject.GetComponent<BlackBomb>(); 
                            bomb.Thrown = true;
    }
                        // Unparent the object
                        objectInHand.SetParent(null);

                        // Add a Rigidbody component to the object if it doesn't have one
                        Rigidbody rb = objectInHand.gameObject.GetComponent<Rigidbody>();
                        if (rb == null)
                        {
                            rb = objectInHand.gameObject.AddComponent<Rigidbody>();
                        }

                        rb.isKinematic = false;

                        // Throw the object forward
                        float throwForce = 500.0f; // You can adjust this value as needed
                        rb.AddForce(playerHand.forward * throwForce);
                        withObject = false;

                    }
                }
            }
            Debug.DrawRay(transform.position + new Vector3(0, heightOffset, 0), transform.forward * range, Color.red);
        }

    }
}
