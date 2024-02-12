using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class Damage : MonoBehaviour
    {
        public float damage = 100f;
        void OnTriggerEnter(Collider other)
        {
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            if (player != null)
            {
                player.GetDamage(damage);
            }
        }
    }
}
