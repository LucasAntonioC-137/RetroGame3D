using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using UnityEngine.InputSystem.Android.LowLevel;

public class PatrolingMisery : MonoBehaviour
{
    public GameObject pointA, pointB;
    public Transform headPoint;
    public int Score;
    public float speed;
    private Rigidbody2D rig;
    private BoxCollider2D boxCol;
    private CircleCollider2D cirCol;
    private Animator anim;
    private Transform currentPoint;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        cirCol = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rig.velocity = new Vector2(speed, 0);
        }
        else
        {
            rig.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        }
    }

    bool playerDestroyed;
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0 && !playerDestroyed)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                anim.SetTrigger("death");
                AudioController.current.PlayMusic(AudioController.current.jumpInEnemySFX);
                speed = 0;
                boxCol.enabled = false;
                cirCol.enabled = false;
                Destroy(transform.parent.gameObject, 1.35f);
                EnvironmentController.instance.playerScore +=  Score;
                EnvironmentController.instance.UpdateScoreText();
            }else
            {
                playerDestroyed = true;
                AudioController.current.PlayMusic(AudioController.current.deathSFX);
                EnvironmentController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
