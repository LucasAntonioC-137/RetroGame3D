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

        public float speed = 2f;
        public float jumpHeight = 2.0f;
        public float gravityValue = -9.81f;
        public float rotationSpeed;
        public float jumpForce = 3.0f;
        public bool action;
        public float verticalSpeed;
        public float magnitud;

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
            Move();
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

                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
            }

            /*verticalSpeed += gravityValue * Time.deltaTime;
            if (characterController.isGrounded)
            {
                action = false;
                verticalSpeed = -0.5f;
                if (Input.GetKey(KeyCode.W))
                {
                    action = true;
                    moveDirection = Vector3.forward * speed;
                    anim.SetInteger("transition", 1);
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    anim.SetInteger("transition", 0);
                    moveDirection = Vector3.zero;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    action = true;
                    verticalSpeed = jumpForce;
                    anim.SetInteger("transition", 2);
                }
            }

            if (characterController.isGrounded && !action)
            {
                anim.SetInteger("transition", 0);
            }

            rot += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles= new Vector3(0, rot, 0);
            moveDirection.y = verticalSpeed;
            moveDirection = transform.TransformDirection(moveDirection);
            characterController.Move(moveDirection * Time.deltaTime);
           */
        }
    }
}
