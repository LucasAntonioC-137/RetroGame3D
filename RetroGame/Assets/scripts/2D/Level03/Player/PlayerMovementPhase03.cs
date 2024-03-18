using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhase03 : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    private bool isJumping;
    [SerializeField] Rigidbody2D rig;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * playerSpeed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            //anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            //while(Input.GetAxis("Horizontal") < 0) { playerSpeed = playerSpeed / 2; }
            //anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f); //virar o ângulo do sprite, vamos ver isso quando trocar de lado
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            //anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetKey(KeyCode.W) && !isJumping)
        {
            //isJumping = true;
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
            //anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }
}
