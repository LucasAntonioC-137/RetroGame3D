using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sympathy : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;
    public int Score;
    public float speed, JumpForce, timeLeft;
    public bool timerOn = false;

    public Transform rightCol, leftCol, headPoint;

    private bool colliding; //, Jump;

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

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }

        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {   //pulo
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
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                anim.SetTrigger("death");
                Destroy(gameObject, 0.34f);
                EnvironmentController.instance.playerScore +=  Score;
                EnvironmentController.instance.UpdateScoreText();
            }
        }
    }
}