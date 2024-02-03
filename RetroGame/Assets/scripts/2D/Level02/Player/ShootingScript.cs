using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //public GameObject Projectile;
    public GameObject Bullet;
    public GameObject bulletSpawn;
    // public GameObject BoltHit;

    public float bulletSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        if(Input.GetKeyDown(KeyCode.Space)) //&& inputVector.x != 0f & inputVector.y != 0f)
        {

            //GameObject projectile = Instantiate(Projectile);
            GameObject projectile = Instantiate(Bullet);
            projectile.transform.position = bulletSpawn.transform.position;
            projectile.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(inputVector));

            //projectile.GetComponent<Rigidbody2D> ().AddForce(transform.forward * bulletSpeed, ForceMode2D.Impulse);
            projectile.GetComponent<Rigidbody2D> ().AddForce(inputVector * bulletSpeed, ForceMode2D.Impulse);

            Destroy(projectile.gameObject, 2f);

        }
        
    }

    //CodeMonkey script - por nas referências
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //      if(collision.gameObject.layer == 15){
    //         //Debug.Log("colidi");
    //         //Instantiate(BoltHit, this.transform.position, Quaternion.identity);
    //         Destroy(gameObject, 0.2f);
    //     }
    // }    
}
