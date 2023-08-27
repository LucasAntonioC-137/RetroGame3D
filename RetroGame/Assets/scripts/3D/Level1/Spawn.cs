using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RailShooter
{
    [System.Serializable]
    public class Spawn
    {
        public float distance; // Distância do centro do quadrado em relação ao gerador
        public float sideLength; // Comprimento do lado do quadrado

        public Vector3 GetRandomPoint()
        {
            // Coordenadas x e y aleatórias entre -sideLength/2 e sideLength/2
            float x = Random.Range(-sideLength / 2, sideLength / 2);
            float y = Random.Range(-sideLength / 2, sideLength / 2);

            // Retorna o ponto
            return new Vector3(x, y, distance);
        }
    }
}
