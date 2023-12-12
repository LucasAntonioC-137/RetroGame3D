using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoneBlock : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField]
    private Vector2 boxSize;

    [SerializeField]
    private Collision2D playerCollider;


    // Update is called once per frame
    void Update()
    {
      //OnCollisionEnter2D(playerCollider);
      
    }
}

    //lembrar de re-ver namespace pra não repetir o código

  
//     private void IsPushing(){
//         //Collider2D colisor = Physics2D.OverlapBox(transform.position, boxSize);
//         if(colisor.gameObject.CompareTag("Player") == false){
//             rig.velocity = Vector2.zero; //(0, 0)
//         }
//     }
// }
