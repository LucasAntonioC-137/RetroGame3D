using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform Player;
    public float minX, maxX;
    public float timeLerp; //movimentação da câmera
    //public float smoothSpeed;

    //private Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 newPosition = Player.position + new Vector3(0,0,-10); 
        newPosition.y = -0.2f;
        transform.position = newPosition;
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        transform.position = newPosition;
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
         transform.position.y, transform.position.z);
    }
}
