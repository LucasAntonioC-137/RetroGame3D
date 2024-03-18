using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform punchAttack;
    public float punchRange = 0.5f;

    public float atkRate = 2f;
    public float nextAtkTime = 0f;

    public LayerMask enemyLayer;
    void Update()
    {
        if(Time.time >= nextAtkTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAtkTime= Time.time + 1 / atkRate;
            }
        }
       
    }

    void Attack()
    {
        //play attack animation
        //detect enemies in range of attack
        Physics2D.OverlapCircleAll(punchAttack.position, punchRange, enemyLayer);

        //damage them
        Debug.Log("We hit him");
        //anim.SetTrigger("Attack1);


    }

    private void OnDrawGizmosSelected()
    {
        if (punchAttack == null)
            return;

        Gizmos.DrawWireSphere(punchAttack.position, punchRange);
    }
}
