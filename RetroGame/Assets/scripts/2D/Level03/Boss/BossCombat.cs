using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [SerializeField] Transform punchAttack;
    public float punchRange = 0.5f;
    private float punchDamage = 10;
    public float repulsionForce = 100f;
    
    public float repulsionY = 0.9f;
    public float repulsionX = -4f;

    public LayerMask playerLayer;
    private PlayerWalk playerDirection;

    //para defesa do player ativar
    private Animator bossAnim;

    //LEMBRANDO QUE PRECISO DESATIVAR MOMENTANEAMENTE
    //O AGGRO DO CHEFE QUANDO ELE EESTIVER RECEBENDO GOLPES DO PLAYER
    private void Awake()
    {
        bossAnim = GetComponentInChildren<Animator>();
        playerDirection = GameObject.Find("Player").GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bossAttack1();
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
            Rigidbody2D enemyRb = playerCol.GetComponent<Rigidbody2D>();

            if (PlayerDefense.isDefending == true && enemyRb != null)
            {
                //Debug.Log(PlayerDefense.isDefending); //não tá chegando aqui como true, apenas quando eu ataco duas vezes seguidas rapidamente
                player.TakeDamage(punchDamage / 2); //diminuir o dano pegando a variável isDefending
                //Debug.Log("Entramos no while");
            }
            else if (PlayerDefense.isDefending == false && enemyRb != null)
            {
                float directionX;
                if(playerDirection.isFacingRight == true) { directionX = repulsionX; } else {  directionX = -repulsionX; }


                Vector2 repulsionDirection = (Vector2)enemyRb.position - (Vector2)punchAttack.position;
                repulsionDirection.Normalize();

                
                repulsionDirection.y += repulsionY;
                repulsionDirection.x += directionX;//repulsionX;

                //if (PlayerDefense.isDefending == false)
                //{
                //Debug.Log("recebemos o ataque");
                player.TakeDamage(punchDamage);
                enemyRb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force);
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
                enemyRb.AddForce(repulsionDirection * (repulsionForce), ForceMode2D.Force);
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
                enemyRb.AddForce(repulsionDirection * (repulsionForce * 0.2f), ForceMode2D.Force);
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
                enemyRb.AddForce(repulsionDirection * (repulsionForce), ForceMode2D.Force);

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
                enemyRb.AddForce(repulsionDirection * (repulsionForce * 3f), ForceMode2D.Force);

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (punchAttack == null)
            return;

        Gizmos.DrawWireSphere(punchAttack.position, punchRange);
    }
}
