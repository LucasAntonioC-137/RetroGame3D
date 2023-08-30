using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sympathy : MonoBehaviour
{
    
    private Rigidbody2D rig;
    private Animator anim;

    public float speed, JumpForce;
    public float timeLeft;
    public bool timerOn = false;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    private bool colliding;

    public bool jump;

    public LayerMask layer;

    void Start()
    {
        timerOn = true;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }

        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                anim.SetBool("Jump", true);
                Debug.Log("Contador chegou ao fim");
                timeLeft = 3.5f;
            }
            
        }
        
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            anim.SetBool("Jump", false);
        }
    }
}