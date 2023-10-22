using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class PlayerBase : MonoBehaviour
    {
        public Animator animator;
        private float fireTimer;
        private float fireRate = 0.5f;
        public GameObject bulletPrefab;
        public Transform firePoint;
        public int bulletSpeed = 50;
        public int life = 100;
        public int armor = 0;
        public int ammoCount = 10;


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Shot();
            }
            if(fireTimer < fireRate)
            {
                fireTimer += Time.deltaTime;
            }
        }

        public void GetDamage(int damage)
        {
            Debug.Log("Tomei " + damage);
            life -= damage;
            if (life <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            GameObject.Destroy(gameObject);
        }
        void Shot()
        {
            if(fireTimer < fireRate)
            {
                return;
            }
            if (ammoCount > 0)
            {
                animator.CrossFadeInFixedTime("PlayerShot", 0.01f);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                Debug.Log("Atirou");
                if (rb != null)
                {
                    rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
                }
                ammoCount--;
            }
            fireTimer= 0f;
        }
    }
}
