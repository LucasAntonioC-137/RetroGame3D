using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStrike : MonoBehaviour
{
    [SerializeField] private int spDamage;
    [SerializeField] private float spLifeTime;
    [SerializeField] private float spDistance;

    private SpecialBarAttack spBar;

    public GameObject BoltHit;

    public LayerMask enemyLayer;
    
    void Start()
    {
        Invoke("SpDestroy", spLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, spDistance, enemyLayer);

        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Life>().TakeDamage(spDamage);
            }

            //SpDestroy(BoltHit);
        }
    }

    void SpDestroy(GameObject btHit)
    {
        if(btHit.CompareTag("Enemy") || btHit.gameObject.layer == 6)
        {
            BoltHit = Instantiate(BoltHit, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.08f);
            Destroy(BoltHit, 0.3f);
        }
        
        //spBar.spIsActive = false;

        //Destroy(gameObject, 0.4f);

        //BoltHit = Instantiate(BoltHit, gameObject.transform.position, Quaternion.identity);
        //Destroy(gameObject, 0.05f);
        //Destroy(BoltHit, 0.3f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        SpDestroy(collision.gameObject);

        //if (collision.gameObject.layer == 15 || collision.gameObject.CompareTag("Enemy"))
        //{
        //    BoltHit = Instantiate(BoltHit, gameObject.transform.position, Quaternion.identity);
        //    Destroy(gameObject, 0.05f);
        //    Destroy(BoltHit, 0.3f);
        //}
    }
}
