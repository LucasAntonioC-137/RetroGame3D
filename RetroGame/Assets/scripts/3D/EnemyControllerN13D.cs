using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerN13D : MonoBehaviour
{
    public float speed = 5f; // Velocidade do inimigo
    public Vector3[] waypoints; // Pontos pelos quais o inimigo deve passar
    private int currentWaypointIndex = 0; // Índice do waypoint atual

    void Update()
    {
        // Mover o inimigo em direção ao waypoint atual
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);

        // Verificar se o inimigo chegou ao waypoint atual
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            // Avançar para o próximo waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void OnBecameInvisible()
    {
        // Fazer o inimigo desaparecer quando ele sair da tela
        gameObject.SetActive(false);
    }
}
