using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class DoorController : MonoBehaviour
    {
        private Animator animator; // Vari�vel para o componente Animator

        void Start()
        {
            animator = GetComponent<Animator>(); // Obtenha o componente Animator
        }

        public void Open()
        {
            animator.Play("OpenDoor"); // Reproduza a anima��o "Open"
        }
    }
}