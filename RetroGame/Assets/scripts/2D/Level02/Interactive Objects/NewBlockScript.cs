using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlockScript : MonoBehaviour
{
    // public float moveSpeed = 5f;
    // public Transform movePoint;
    public float distance = 1f;
    public LayerMask boxMask;
    // public LayerMask whatStopsMovement;
    public Transform player;

    void Start()
    {
        //movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.left * transform.localScale.x, distance);
    }
}
