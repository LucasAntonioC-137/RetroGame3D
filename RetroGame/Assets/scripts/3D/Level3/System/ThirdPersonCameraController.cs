using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        public Transform target;
        public float distance = 5.0f;
        public float height = 2.0f;
        public float damping = 5.0f;
        public bool smoothRotation = true;
        public float rotationDamping = 10.0f;
        public float deadZone = 0.3f; // Adicione um valor para a "zona morta"
        public float minDistance = 1.0f; // Adicione uma distância mínima
        public float maxDistance = 10.0f; // Adicione uma distância máxima

        private Vector3 lastTargetPosition;
        private float timeToMove;

        void Update()
        {
            Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
            Vector3 currentPosition = transform.position;

            // Verifique se o personagem se moveu para fora da "zona morta"
            if (Vector3.Distance(lastTargetPosition, target.position) > deadZone)
            {
                currentPosition = Vector3.Lerp(currentPosition, wantedPosition, Time.deltaTime * damping);
                lastTargetPosition = target.position;
                timeToMove = Time.time + 0.5f; // Adicione um atraso para a câmera começar a se mover
            }

            if (Time.time > timeToMove)
            {
                Vector3 targetPosition = Vector3.Lerp(currentPosition, target.position, Time.deltaTime * damping);
                // Verifique se a câmera não está muito perto do personagem
                if (Vector3.Distance(targetPosition, target.position) > minDistance)
                {
                    currentPosition = targetPosition;
                }
            }

            // Verifique se o personagem está muito longe da câmera
            if (Vector3.Distance(currentPosition, target.position) > maxDistance)
            {
                currentPosition = Vector3.Lerp(currentPosition, target.position, Time.deltaTime * damping);
            }

            transform.position = Vector3.Lerp(transform.position, currentPosition, Time.deltaTime * damping);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else transform.LookAt(target, target.up);
        }
    }
}