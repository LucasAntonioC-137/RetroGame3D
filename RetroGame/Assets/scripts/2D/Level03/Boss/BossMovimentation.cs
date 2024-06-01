using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMovimentation : StateMachineBehaviour
{
    public float speed = 2f;
    public float attackRange = 3f;
    public int randomBehavior;
    private float timeToAction = 3.5f;
    private float tempoPassado = 0f;
    private Vector2 Backdash = new Vector2 (7f, 0f);
    //private BossCombat bossCombat;

    Transform player;
    Rigidbody2D rb;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();

        //bossCombat = animator.GetComponentInParent<BossCombat>();

        //animator.SetBool("isIddle", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);


        if (Vector2.Distance(player.position, rb.position) <= attackRange)// && i >= timeToAction)
        {
            tempoPassado += Time.fixedDeltaTime;
            if(tempoPassado > timeToAction)
            {
                //randomBehavior = Random.Range(0, 4);
                randomBehavior = Random.Range(2, 3);

                switch (randomBehavior)
                {
                    case 0:
                        Debug.Log("Nesse aqui ele vai esperar parado ou executar uma ação aleatória");
                        //dash pra trás
                        if (player.GetComponentInChildren<PlayerWalk>().isFacingRight == false)
                        {
                            rb.AddForceAtPosition(-Backdash, rb.position, ForceMode2D.Impulse);
                            //colocar corrotina de ficar parado por 1 segundo depois do backdash
                        }
                        else
                        {
                            rb.AddForceAtPosition(Backdash, rb.position, ForceMode2D.Impulse);
                        }
                        //
                        break;
                    case 01:
                        animator.SetTrigger("attack1");
                        new WaitForSeconds(1);
                        break;
                    case 02:
                        animator.SetTrigger("attack2");  //colocar set trigger pra animação do ataque 2 com um código pra ele lá no bossCombat
                        new WaitForSeconds(1);
                        break;
                    case 03:
                        Debug.Log("ataque3");
                        new WaitForSeconds(1);
                        break;
                }
                tempoPassado = 0f;
            }
            //bossCombat.bossPunch1();//tá acertando múltiplas vezes por estar no método UPDATE, preciso fazer ele acertar só uma vez
        }
        
        // if(Input.GetKeyDown(KeyCode.M)) { animator.SetBool("isIddle", true); } 
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
    }

    IEnumerator waitForNewCommand()
    {
        rb.position = Vector2.zero;
        yield return new WaitForSeconds(2f);
    }
}
