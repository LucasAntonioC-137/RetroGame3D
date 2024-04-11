using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [SerializeField] Transform punchAttack;
    public float punchRange = 0.5f;
    private int punchDamage = 10;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (punchAttack == null)
            return;

        Gizmos.DrawWireSphere(punchAttack.position, punchRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Life player = collision.GetComponent<Life>();
        if(player != null && collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(punchDamage);
            Debug.Log(player.life);
            
        }
    }
}
