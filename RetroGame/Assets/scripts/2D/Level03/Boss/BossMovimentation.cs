using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMovimentation : StateMachineBehaviour
{
    public float speed = 2f;
    public float attackRange = 3f;
    public int randomBehavior;
    public int randomSpeed;
    private float timeToAction = 3.5f;
    private float tempoPassado = 0f;
    private Vector2 Backdash;// = new Vector2(7f, 0f);
    int lastState;
    //private BossCombat bossCombat;
    private Life bossSpecialBar;

    Transform player;
    Rigidbody2D rb;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponentInParent<Rigidbody2D>();
        bossSpecialBar = GameObject.Find("Enemy Test").GetComponent<Life>();

        randomSpeed = Random.Range(1, 3);
        if(randomSpeed == 1) { speed = 2f; } else { speed = 3f; }
        //bossCombat = animator.GetComponentInParent<BossCombat>();

        //animator.SetBool("isIddle", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tempoPassado += Time.fixedDeltaTime;
        //regulagem de velocidade
        if (tempoPassado > 2.5f) { speed = 2f; }
        
        //Back walking behavior
        if (lastState == 5)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, -speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            tempoPassado += Time.fixedDeltaTime;
            if(tempoPassado >= timeToAction) 
            {
                newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        


        if (Vector2.Distance(player.position, rb.position) <= attackRange)// && i >= timeToAction)
        {
            //tempoPassado += Time.fixedDeltaTime;
            if (tempoPassado > timeToAction)
            {
                //if(tempoPassado > 1.5f) { speed = 2f; }
                //Testes
                randomBehavior = Random.Range(0, 1);

               //DEFINITIVO
               //randomBehavior = Random.Range(1, 6);

               lastState = randomBehavior; //usado para mudança de velocidade

                switch (randomBehavior)
                {
                    case 0:
                        Debug.Log("case 0");
                        //dash pra trás
                        Backdash = -rb.velocity.normalized;

                        if (player.position.x < rb.position.x)
                        {
                            Debug.Log("ENTREI NO BACKDASH");
                            rb.AddForce(Backdash *  4f, ForceMode2D.Impulse);
                            //colocar corrotina de ficar parado por 1 segundo depois do backdash
                        }
                        else if(player.position.x > rb.position.x)
                        {
                            rb.AddForce(Backdash * 4f, ForceMode2D.Impulse);
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
                        animator.SetTrigger("bossCombo");
                        new WaitForSeconds(1);
                        break;
                    case 04: //está funcionando mas lembre-se que precisa da barrinha dele estar cheia!
                        if (bossSpecialBar.special >= 100)
                        {
                            Debug.Log("Boss Soltou seu special");
                            bossSpecialBar.special = 0f;
                            //animator.SetTrigger("bossSpecialAttack");
                            new WaitForSeconds(4);
                        }
                        break;
                    case 05:
                        animator.SetTrigger("bossWalkBack");
                        tempoPassado = 0;
                        break;
                }
                //if (randomBehavior == 5)
                //{
                //    rb.MovePosition(-newPos);

                //    timeToAction += 2f;
                //    tempoPassado = 0;
                //}
                //else tempoPassado = 0f;
                tempoPassado = 0;

            }
            //bossCombat.bossPunch1();//tá acertando múltiplas vezes por estar no método UPDATE, preciso fazer ele acertar só uma vez
        }

    }

    // if(Input.GetKeyDown(KeyCode.M)) { animator.SetBool("isIddle", true); } 


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("bossCombo");
        animator.ResetTrigger("bossWalkBack");
        animator.ResetTrigger("bossBackdash");
        animator.ResetTrigger("bossSpecialAttack");
    }

}
