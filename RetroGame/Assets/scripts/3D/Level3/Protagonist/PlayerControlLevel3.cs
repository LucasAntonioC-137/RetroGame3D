using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class Level3Protagonist : MonoBehaviour
    {
        private CharacterController characterController;
        private Animator anim;

        private float rot;
        private Vector3 moveDirection;
        private Rigidbody rigidbod;
        private Vector3 velocity;


        public float speed = 2f;
        public float jumpHeight = 2.0f;
        public float gravityValue = -9.81f;
        public float rotationSpeed;
        public bool isJumping;
        public float verticalSpeed;
        public float magnitud;
        public float maxJumpTime = 0.5f; // O tempo máximo que o personagem pode passar no ar
        public float maxJumpForce = 10f; // A força máxima do salto
        public AnimationCurve jumpCurve; // Uma curva de animação que determina a força do salto em função do tempo


        // Start is called before the first frame 

        void Start()
        {
            characterController= GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            anim.SetInteger("transition", 0);
        }

        // Update is called once per frame
        void Update()
        {
            characterController.transform.position = characterController.transform.position;
            Move();
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

                // Move the character in the new direction
                characterController.Move(movementDirection * Time.deltaTime * speed);

                // If there is any movement, rotate the character to face the movement direction
                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
                if (!isJumping)
                {
                    anim.SetInteger("transition", 1);
                }
            }
            else if(!isJumping)
            {
                anim.SetInteger("transition", 0);
            }

            // Check for jump input
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                velocity.y = maxJumpForce; // Aplica a força do pulo
                isJumping = true;
                anim.SetInteger("transition", 2); // Assuming '2' is your jump animation
            }

            // Check if the character has landed
            if (characterController.isGrounded)
            {
                isJumping = false;
            }
            // Aplica a gravidade
            velocity.y += gravityValue * Time.deltaTime;

            // Move o personagem
            //characterController.Move(velocity * Time.deltaTime);
        }
        IEnumerator Jump()
        {
            float timeInAir = 0.0f;
            while (Input.GetKey(KeyCode.Space) && timeInAir < maxJumpTime)
            {
                float jumpForce = jumpCurve.Evaluate(timeInAir / maxJumpTime) * maxJumpForce;
                characterController.Move(new Vector3(0, jumpForce, 0) * Time.deltaTime);
                timeInAir += Time.deltaTime;
                yield return null;
            }
        }

    }
}
