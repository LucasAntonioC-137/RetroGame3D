using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovimentation : StateMachineBehaviour
{
    public float speed = 2f;
    public float attackRange = 3f;
    private BossCombat bossCombat;

    Transform player;
    Rigidbody2D rb;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
        bossCombat = animator.GetComponentInParent<BossCombat>();
        //animator.SetBool("isIddle", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack");
            bossCombat.bossPunch1();//tá acertando múltiplas vezes por estar no método UPDATE, preciso fazer ele acertar só uma vez
        }
       // if(Input.GetKeyDown(KeyCode.M)) { animator.SetBool("isIddle", true); } 
        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }

}
