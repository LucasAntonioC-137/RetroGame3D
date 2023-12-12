using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public Transform target;
    public Vector3 playerChange;
    private BasicCameraFollow cam; //script da câmera

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<BasicCameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.transform.position += playerChange;
        }
    }

      private void OnDrawGizmosSelected() {
            Gizmos.DrawWireCube(playerChange, playerChange);
            
        }
}
