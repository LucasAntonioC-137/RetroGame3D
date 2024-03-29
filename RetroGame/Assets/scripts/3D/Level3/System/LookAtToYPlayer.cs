using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class LookAtToYPlayer : MonoBehaviour
    {
        public Transform player; // O Transform do seu jogador
        private Vector3 lookAtPosition;

        void LateUpdate()
        {
            // Atualiza a posi��o Y do objeto "Look At" para corresponder � posi��o Y do jogador
            lookAtPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = lookAtPosition;
        }
    }

}