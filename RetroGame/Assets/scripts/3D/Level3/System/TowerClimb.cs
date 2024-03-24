using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class TowerClimb : MonoBehaviour
    {
        public CinemachineVirtualCamera baseCamera;
        public CinemachineVirtualCamera towerCamera;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                baseCamera.Priority = 0;
                towerCamera.Priority = 1;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                baseCamera.Priority = 1;
                towerCamera.Priority = 0;
            }
        }
    } 
}
