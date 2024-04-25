using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public Rigidbody2D rbody;
    public int playerSpeed, jumpForce;
    public bool isJumping;
    public Animator anim;

    //Este é um script de movimentação simples (praticamente o mesmo que usei no nível 1)
    //preciso adaptar para ser o mais próximo possível de um jogo de luta
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate( *)
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        if (movement.x > 0) { anim.Play("run"); transform.eulerAngles = new Vector3(0f, 0f, 0f); } 
        else if (movement.x < 0) { anim.Play("run"); transform.eulerAngles = new Vector3(0f, 180f, 0f); }

        transform.position += movement * Time.deltaTime * playerSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            //anim.SetBool("jump", true);
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
}
