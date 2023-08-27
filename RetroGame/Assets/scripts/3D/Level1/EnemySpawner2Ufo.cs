using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


namespace RailShooter
{
    public class EnemySpawner2Ufo : MonoBehaviour
    {
        [SerializeField] GameObject square1;
        [SerializeField] GameObject square2;
        [SerializeField] Spawn[] spawn;
        [SerializeField] EnemyBase enemyPrefab;
        [SerializeField] float spawnInterval = 5f;

        [SerializeField] Transform enemyParent;
        [SerializeField] Transform flightPathParent;

        float spawnTimer;

        void Update()
        {
            if (spawnTimer > spawnInterval)
            {
                spawnTimer = 0f;
                SpawnEnemy();
            }
            spawnTimer += Time.deltaTime;
        }

        void SpawnEnemy()

        {
            var flightPaths = FlightPathFactory.GenerateFlightPath(square1.transform.position, square2.transform.position);
            EnemyFactory.GenerateEnemyBase(enemyPrefab, flightPaths, enemyParent, flightPathParent);

        }
    }
}
