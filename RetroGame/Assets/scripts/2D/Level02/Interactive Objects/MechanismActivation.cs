using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MechanismActivation : MonoBehaviour
{
    public GameObject stoneBlock;
    public int mechanismCount;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Pushable")){
            Debug.Log("ativou mechanismo");
            collision.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
            collision.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
            mechanismCount += 1;
            collision.gameObject.tag = "Activated";
            collision.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
        }
        Debug.Log(mechanismCount);
    }
}
