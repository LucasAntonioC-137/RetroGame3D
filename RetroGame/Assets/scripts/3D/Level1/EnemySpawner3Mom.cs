using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


namespace RailShooter
{
    public class EnemySpawner3Mom : MonoBehaviour
    {
        [SerializeField] Square[] spawn;
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
            var flightPaths = FlightPathFactory.GenerateFlightPath(spawn);
            EnemyFactory.GenerateEnemyBase(enemyPrefab, flightPaths, enemyParent, flightPathParent);

        }
    }
}
