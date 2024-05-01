using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public Rigidbody2D rbody;
    public int playerSpeed, jumpForce;
    public bool isJumping;
    public bool isFacingRight; // começa como true apenas porque ele é o jogador e por conveniência 
    public Animator anim;             // ele vai começar do lado esquerdo
    public SpriteRenderer spriteRenderer;
    private bool canMove = true;

    Vector3 movement;
    //Este é um script de movimentação simples (praticamente o mesmo que usei no nível 1)
    //preciso adaptar para ser o mais próximo possível de um jogo de luta
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate( *)
        Move();
        Jump();
        Facing();
    }

    void Move()
    {
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

            switch (isFacingRight)
            {
                case true: //olhando para a direita
                    if (movement.x > 0)
                    {
                        anim.SetBool("isRunning", true);
                    }
                    else if (movement.x < 0)
                    {
                        anim.SetBool("runningBack", true);
                    }
                    else
                    {
                        anim.SetBool("isRunning", false);
                        anim.SetBool("runningBack", false);
                    }
                    break;
                case false: //olhando para a esquerda
                    if (movement.x > 0)
                    {
                        anim.SetBool("runningBack", true);
                    }
                    else if (movement.x < 0)
                    {
                        anim.SetBool("isRunning", true);
                    }
                    else
                    {
                        anim.SetBool("isRunning", false);
                        anim.SetBool("runningBack", false);
                    }
                    break;
            }

            transform.position += movement * Time.deltaTime * playerSpeed;
        }
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
           //deactivate();
            //anim.SetBool("jump", true);
        }
    }

    #region Direção da Visão do jogador
    public GameObject Enemy;
    private float fightersDistance;
    void Facing()
    {
        Vector3 scale = transform.localScale;

        if (Enemy.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
            isFacingRight = true;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            isFacingRight = false;
        }
        

        transform.localScale = scale;

    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
            //anim.SetBool("jump", false);
        }
    }

    IEnumerator deactivate()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        StopCoroutine(deactivate());
    }
}
