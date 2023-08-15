using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    public GameObject MainCamera;
    private float length, startPos;
    public float speedParallax;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
         float temp = (MainCamera.transform.position.x * (1 - speedParallax));
        float dist = (MainCamera.transform.position.x * speedParallax);

        transform.position = new Vector3 (startPos + dist, transform.position.y, transform.position.z);

        if(temp > startPos + length)
        {
            startPos += length;    
        }else if(temp < startPos - length){
            startPos -= length;
        }
    }
}
