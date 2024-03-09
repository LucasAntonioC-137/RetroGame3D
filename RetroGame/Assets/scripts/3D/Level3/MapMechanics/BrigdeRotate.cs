using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Level3
{
    public class BrigdeRotate : MonoBehaviour
    {
        public float rotationSpeed = 2f;
        private Transform bridge;

        // Start is called before the first frame update
        void Start()
        {
            bridge = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            bridge.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}