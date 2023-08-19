using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForward : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float laserSpeed = 10f;
    public float bombSpeed = 3f;
    public GameObject laserPrefab;
    public Transform firePoint; // ponto de referência para os tiros

    public float bombFireRate = 1f;
    public GameObject bombPrefab;

    private float nextFire = 0.0f;
    private float nextBombFire = 0.0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireLaser();
        }

        if (Input.GetButton("Fire2") && Time.time > nextBombFire)
        {
            nextBombFire = Time.time + bombFireRate;
            DropBomb();
        }
    }
    void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation) as GameObject;
        laser.GetComponent<Rigidbody>().velocity = firePoint.forward * laserSpeed;
    }

    void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        bomb.GetComponent<Rigidbody>().velocity = firePoint.forward * bombSpeed;
    }
}
