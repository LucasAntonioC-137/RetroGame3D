using level2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class BulletDamage : MonoBehaviour
    {
        public int damage = 20;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Causou " + damage);
                EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.GetDamage(damage);
                }
            }
        }
    }
}
