using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDefense : MonoBehaviour
{
    public static bool isDefending = false; //utilizar para desativar a possibilidade de movimentação na defesa
    private PlayerWalk walk;
    public Rigidbody2D weight; //pego no editor
    
    private void Awake()
    {
        walk = GameObject.Find("Player").GetComponent<PlayerWalk>();
        //playerCol = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDefending == true) 
        { 
            Defending();
        } else isDefending = false;
    }

    IEnumerator DefenseEndCo()
    {
       
        yield return new WaitForSeconds(0.6f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("EnemyAttack")) { Debug.Log("eu existo"); return; } //funcionou, agora por que não funciona quando peço pra
                                                                                                //testar com o player andando pra trás? Resposta: precisava instanciar o script
                                                                                                //no awake pra que o IF "soubesse" que a variável isFacingRight existe
        if (collision.gameObject.CompareTag("EnemyAttack") &&
        (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
        Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador tá andando pra trás && (FUNCIONANDO)
        {
            Debug.Log("Defendemos!");
            isDefending = true; //fazer durar uma quantidade específica de frames, USAREMOS ESSA BOOL pra manter um while ligado, depois que ela mudar de valor ele acaba 
            //StartCoroutine(DefenseCo());
        }
        else if (collision.gameObject.CompareTag("EnemyAttack") &&
                ((Input.GetAxis("Horizontal") == 0 ||
                ((Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == true) || (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == false)))))
        {
            isDefending = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack") || 
            (collision.gameObject.CompareTag("EnemyAttack") && (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == true
                                                             || (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == false))))
        {
            StartCoroutine(DefenseEndCo());
            isDefending = false;
        }
    }

    void Defending()
    {
        weight.velocity = Vector2.zero;

        weight.GetComponent<Rigidbody2D>().IsSleeping();
        
        weight.GetComponent<Rigidbody2D>().IsAwake(); //deu quase bom, ainda temos problema com o dano saindo menor mesmo quando o player está indo pra frente do ataque
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (collision.gameObject.CompareTag("EnemyAttack") &&
    //        (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
    //         Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador tá andando pra trás && )
    //    {
    //        Debug.Log("Defendemos!");
    //        isDefending = true; //fazer durar uma quantidade específica de frames
    //    }


    //    if (collision.gameObject.CompareTag("EnemyAttack")){ Debug.Log("mizera"); }
    //    //(movement.x < 0 && isFacingRight == true ||
    //    // movement.x > 0 && isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador tá andando pra trás && )
    //    //{
    //    //    Debug.Log("Defendemos!");
    //    //    isDefending = true; //fazer durar uma quantidade específica de frames
    //    //}

    //}


}
