using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawer : MonoBehaviour
{
    public float bombSpeed = 3f;
    public float bombFireRate = 1f;
    public GameObject bombPrefab;
    private float nextBombFire = 0.0f;
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && Time.time > nextBombFire)
        {
            nextBombFire = Time.time + bombFireRate;
            DropBomb();
        }
    }
    void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        bomb.GetComponent<Rigidbody>().velocity = firePoint.forward * bombSpeed;
    }
}
