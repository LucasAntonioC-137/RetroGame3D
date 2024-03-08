using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        public float rotationSpeed;
        private int jumpCount = 0;
        public bool isJumping;
        public bool withObject;
        public float verticalSpeed;
        public float magnitud;
        public float maxJumpTime = 0.5f; // O tempo máximo que o personagem pode passar no ar
        public float jumpForce = 10f; // A força máxima do salto
        public float maxTimeBetweenJumps = 2;
        public float doubleJumpForce = 15;
        public float knockbackStrength;
        private BlackBomb bomb;

        // Start is called before the first frame 

        void Start()
        {
            characterController= GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            anim.SetInteger("transition", 0);
            jumpCount= 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDead)
            {
                characterController.transform.position = characterController.transform.position;
                Move();
                Take_Object();
            }
            else
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

        }


        public void GetDamage(float damage, Vector3 damageDirection)
        {
            playerHealth -= damage;
            if(playerHealth <= 0)
            {
                PlayerDie();
            }
            Vector3 knockbackDirection = new Vector3(damageDirection.x, 0, damageDirection.z).normalized;
            Vector3 oppositeDirection = -knockbackDirection;
            // Calcula a direção final com 45 graus de inclinação no eixo Y
            Vector3 finalKnockbackDirection = Quaternion.Euler(0, 90, 0) * oppositeDirection;
            // Aplica a força do knockback
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                characterController.enabled= false;
                rb.AddForce(finalKnockbackDirection * knockbackStrength, ForceMode.Impulse);
                print("KockBack");
                StartCoroutine(SetRigidbodyKinematicAfterDelay(rb, 0.2f)); // 1.0f é o atraso em segundos
            }
        }
        IEnumerator SetRigidbodyKinematicAfterDelay(Rigidbody rb, float delay)
        {
            yield return new WaitForSeconds(delay);
            characterController.enabled= true;
            rb.isKinematic = true;
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

            // Aplica a gravidade
            if (!characterController.isGrounded)
            {
                velocity.y += gravityValue * Time.deltaTime;
            }
            else if (velocity.y < 0)
            {
                // Se o personagem está no chão, garante que a velocidade vertical não seja negativa
                velocity.y = 0;
            }

            // Check for jump input
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !withObject)
            {
                if (jumpCount == 0)
                {
                    // Aplica a força do pulo
                    velocity.y = jumpForce;
                    isJumping = true;
                    anim.SetInteger("transition", 2);
                    jumpCount++;
                    timeSinceJump = 0; // Reseta o tempo desde o último pulo
                }
                else if (jumpCount == 1 && timeSinceJump < maxTimeBetweenJumps)
                {
                    velocity.y = doubleJumpForce;
                    isJumping = true;
                    anim.SetInteger("transition", 3);
                    jumpCount++;
                }
            }

            if (timeSinceJump > maxTimeBetweenJumps && characterController.isGrounded)
            {
                jumpCount= 0;
                timeSinceJump = 0;
            }
            if(jumpCount == 1)
            {
                timeSinceJump += Time.deltaTime;
            }

            // Check if the character has landed
            if (characterController.isGrounded && isJumping)
            {
                anim.SetInteger("transition", 1);
                isJumping = false;
                if(jumpCount == 2)
                {
                    jumpCount = 0;
                }
            }
            // Aplica a gravidade
            velocity.y += gravityValue * Time.deltaTime;

            // Adiciona a velocidade ao movimento do personagem
            characterController.Move(velocity * Time.deltaTime);

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
                Vector3 movementDirection = forwardDirection * verticalInput + rightDirection * horizontalInput;
                movementDirection.Normalize();

                if (!isJumping)
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
            else if (!isJumping)
            {
                if (!withObject)
                    anim.SetInteger("transition", 0);
                else
                    anim.SetInteger("transition", 4);
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
