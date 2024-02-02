using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Level3
{
    public class CamRigLevel3 : MonoBehaviour
    {
        [SerializeField] int viewIndex = 0;
        [SerializeField] Transform[] viewPoints;

        private void Start()
        {
            Look();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                viewIndex--;
                if (viewIndex < 0)
                    viewIndex = viewPoints.Length - 1 ;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                viewIndex++;
                if (viewIndex > viewPoints.Length - 1)
                    viewIndex = 0;
            }
            Look();
        }

        /*void Look()
        {
            transform.position = Vector3.Lerp(transform.position, viewPoints[viewIndex].position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, viewPoints[viewIndex].rotation, 0.05f);
        }*/
        
        void Look()
        {
            transform.position = viewPoints[viewIndex].position;
            //print(transform.position);
            transform.rotation = viewPoints[viewIndex].rotation;
            //print(transform.rotation);
        }

    }
}