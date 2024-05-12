using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [SerializeField] Transform punchAttack;
    public float punchRange = 0.5f;
    private int punchDamage = 10;
    public float repulsionForce = 100f;
    public LayerMask playerLayer;

    //para defesa do player ativar
    private Animator bossAnim;
    // Start is called before the first frame update
    private void Awake()
    {
       bossAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bossPunch1();            
        }
    
    }

    void bossPunch1()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);
        StartCoroutine(atk());
        foreach (Collider2D playerCol in hitPlayer)
        {
                
                Life player = playerCol.GetComponent<Life>();
                //aplicar força de repulsão
                Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                    repulsionDirection.Normalize();

                    float repulsionY = 0.7f;
                    repulsionDirection.y += repulsionY;

                    player.TakeDamage(punchDamage);

                    enemyRb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force);
                }
        }
        
    }
    

    private void OnDrawGizmosSelected()
    {
        if (punchAttack == null)
            return;

        Gizmos.DrawWireSphere(punchAttack.position, punchRange);
    }

    IEnumerator atk()
    {
        bossAnim.SetBool("attacking", true);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("attacking", false);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    Debug.Log("entramos no collision");
    //    Life player = collision.GetComponent<Life>();
    //    if(player != null && collision.gameObject.CompareTag("Player"))
    //    {

    //        player.TakeDamage(punchDamage);
    //        Debug.Log("Vida do player:" + player.life);
            
    //    }
    //}
}
