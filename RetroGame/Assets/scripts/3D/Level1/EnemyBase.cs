using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter {
    public class EnemyBase : ValidatedMonoBehaviour {
        [SerializeField, Self] SplineAnimate splineAnimate;
        //[SerializeField] GameObject explosionPrefab;
        SplineContainer flightPath;
        
        public SplineContainer FlightPath {
            get => flightPath;
            set => flightPath = value;
        }
        void Update() {
            if (splineAnimate != null && splineAnimate.ElapsedTime >= splineAnimate.Duration) {
                Destroy(gameObject);
            }

        }

        void OnTriggerEnter(Collider other) {
            //var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            if (other.CompareTag("PlayerBullets"))
            {
                Destroy(gameObject);
                //var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity
                //Destroy(explosion, 5f);
            }
        }
        void OnDestroy()
        {
            if (flightPath != null)
            {
                Destroy(flightPath.gameObject);
            }
        }

    }
}