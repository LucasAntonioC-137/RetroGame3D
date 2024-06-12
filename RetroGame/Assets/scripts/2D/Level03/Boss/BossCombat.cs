using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public LayerMask playerLayer;
    private PlayerWalk playerDirection;
    

    #region Dados de ataque
    [SerializeField] Transform punchAttack;
    public float punchRange = 0.5f;
    private float punchDamage = 10;
    public float repulsionForce = 100f;
    
    public float repulsionY = 0.9f;
    public float repulsionX = -4f;
    #endregion

    #region movimentação do Chefe
    private Rigidbody2D bossRb;
    private Animator bossAnim;

    [SerializeField] float speedDash;// = 3f;
    
    private Vector2 stopMoving = Vector2.zero;
    
    private float originalGravity;
    #endregion

    //LEMBRANDO QUE PRECISO DESATIVAR MOMENTANEAMENTE
    //O AGGRO DO CHEFE QUANDO ELE EESTIVER RECEBENDO GOLPES DO PLAYER
    private void Awake()
    {
        bossRb = GameObject.Find("Enemy Test").GetComponentInParent<Rigidbody2D>();
        bossAnim = GetComponentInChildren<Animator>();
        playerDirection = GameObject.Find("Player").GetComponent<PlayerWalk>();
    }

    private void Start()
    {
        originalGravity = bossRb.gravityScale;
    }

    //Dados funciondo no componente "Sprite" do boss
    public void bossBackdash()
    {

//        bossRb.gravityScale = 0f;
        //bossAnim.SetBool("Backdash", true);
        

        if (playerDirection.isFacingRight == true)
        {
            bossRb.velocity = new Vector2(speedDash, bossRb.velocity.y);            
        } 
        else 
        {
            bossRb.velocity = new Vector2(-speedDash, bossRb.velocity.y);
        }

    }

    public void bossAttack1()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);
        //StartCoroutine(atk());
        foreach (Collider2D playerCol in hitPlayer)
        {

            Life player = playerCol.GetComponent<Life>();
            //aplicar força de repulsão
            Rigidbody2D playerRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == true && playerRb != null)
            {
                //Debug.Log(PlayerDefense.isDefending); //não tá chegando aqui como true, apenas quando eu ataco duas vezes seguidas rapidamente
                player.TakeDamage(punchDamage / 2); //diminuir o dano pegando a variável isDefending
                //Debug.Log("Entramos no while");
            }
            else if (PlayerDefense.isDefending == false && playerRb != null)
            {
                float directionX;
                if(playerDirection.isFacingRight == true) { directionX = repulsionX; } else {  directionX = -repulsionX; }


                Vector2 repulsionDirection = (Vector2)playerRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();

                
                repulsionDirection.y += repulsionY;
                repulsionDirection.x += directionX;//repulsionX;

                //if (PlayerDefense.isDefending == false)
                //{
                //Debug.Log("recebemos o ataque");
                player.TakeDamage(punchDamage);
                playerDirection.canMove = false;

                playerRb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force);
                StartCoroutine(takingDamage());
                //}
                //enemyRb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force); //repulsão normal, vou por dentro apenas quando isDefending é false
            }
        }

    }

    void BossAttack2()
    {
        Debug.Log("acertamos o ataque2");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange * 2, playerLayer);
        //StartCoroutine(atk());
        foreach (Collider2D playerCol in hitPlayer)
        {
            Life player = playerCol.GetComponent<Life>();
            //aplicar força de repulsão
            Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == true && enemyRb != null)
            {
                float directionX;
                if (playerDirection.isFacingRight == true) { directionX = repulsionX; } else { directionX = -repulsionX; }

                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();


                repulsionDirection.y += repulsionY;
                repulsionDirection.x += 0.2f;//repulsionX;

                player.TakeDamage(punchDamage / 3);
                enemyRb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force);
                return;
            }
            else if (PlayerDefense.isDefending == false && enemyRb != null)
            {
                //primeiro hit do combo
                float directionX;
                if (playerDirection.isFacingRight == true) { directionX = repulsionX; } else { directionX = -repulsionX; }

                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();

                repulsionDirection.y += 15;
                repulsionDirection.x += directionX;

                player.TakeDamage(punchDamage * 2);
                playerDirection.canMove = false;

                enemyRb.AddForce(repulsionDirection * (repulsionForce), ForceMode2D.Force);
                StartCoroutine(takingDamage());
            }
        }
    }
    //Esse ataque vai começar com uma estocada, se acertar o primeiro golpe, seguiremos pro segundo
    public void bossComboFirstHit()
    {
        
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);
        //StartCoroutine(atk());
        foreach (Collider2D playerCol in hitPlayer)
        {
            Life player = playerCol.GetComponent<Life>();
            //aplicar força de repulsão
            Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == true && enemyRb != null)
            {
                //fazer ele parar o ataque aqui
                return;
            }
            else if (PlayerDefense.isDefending == false && enemyRb != null)
            {
                //primeiro hit do combo
                float directionX;
                if (playerDirection.isFacingRight == true) { directionX = repulsionX; } else { directionX = -repulsionX; }

                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();

                repulsionDirection.y += repulsionY;
                repulsionDirection.x += directionX;//repulsionX;

                player.TakeDamage(punchDamage / 2);
                playerDirection.canMove = false;

                enemyRb.AddForce(repulsionDirection * (repulsionForce * 0.2f), ForceMode2D.Force);
                StartCoroutine(takingDamage());
            }
        }
    }

    public void BossComboSecondHit()
    {

        
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);

        foreach (Collider2D playerCol in hitPlayer)
        {
            Life player = playerCol.GetComponent<Life>();
            //aplicar força de repulsão
            Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == false && enemyRb != null)
            {
                //segundo hit do combo
                float directionX;
                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();
                if (playerDirection.isFacingRight == true) { directionX = repulsionX; } else { directionX = -repulsionX; }

                repulsionDirection.y += 3f;//repulsionY;
                

                player.TakeDamage(punchDamage / 2);
                playerDirection.canMove = false;
                
                enemyRb.AddForce(repulsionDirection * (repulsionForce), ForceMode2D.Force);
                StartCoroutine(takingDamage());
            }
        }
    }

    public void BossComboThirdHit()
    {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(punchAttack.position, punchRange, playerLayer);

        foreach (Collider2D playerCol in hitPlayer)
        {
            Life player = playerCol.GetComponent<Life>();
            //aplicar força de repulsão
            Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == false && enemyRb != null)
            {
                //hit mais forte do combo
                float directionX;
                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();
                if (playerDirection.isFacingRight == true) { directionX = repulsionX; } else { directionX = -repulsionX; }

                repulsionDirection.y += repulsionY;
                repulsionDirection.x += directionX;

                player.TakeDamage(punchDamage);
                playerDirection.canMove = false;
                
                enemyRb.AddForce(repulsionDirection * (repulsionForce * 3f), ForceMode2D.Force);
                StartCoroutine(takingDamage());

            }
        }
    }

    IEnumerator takingDamage()
    {
        yield return new WaitForSecondsRealtime(1f);
        playerDirection.canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (punchAttack == null)
            return;

        Gizmos.DrawWireSphere(punchAttack.position, punchRange);
    }
}
