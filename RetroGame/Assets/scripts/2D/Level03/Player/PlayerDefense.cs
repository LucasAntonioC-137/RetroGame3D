using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDefense : MonoBehaviour
{
    public static bool isDefending = false; //utilizar para desativar a possibilidade de movimenta��o na defesa
    private PlayerWalk walk;
    public Rigidbody2D weight; //pego no editor

    private void Awake()
    {
        walk = GameObject.Find("Player").GetComponent<PlayerWalk>();
        //playerCol = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        ActiveDefense();
    }
    void FixedUpdate()
    {
        //if (isDefending == true)
        //{
        //    Defending();
        //}
        //else isDefending = false;
    }

    IEnumerator DefenseEndCo()
    {

        yield return new WaitForSeconds(0.6f);
        isDefending = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("EnemyAttack")) { Debug.Log("eu existo"); return; } //funcionou, agora por que n�o funciona quando pe�o pra
        //testar com o player andando pra tr�s? Resposta: precisava instanciar o script
        //no awake pra que o IF "soubesse" que a vari�vel isFacingRight existe

        //if (collision.gameObject.CompareTag("EnemyAttack"))// &&
        //(Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
        //Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador t� andando pra tr�s && (FUNCIONANDO)
        //{
        Defending(collision.gameObject);
  

        //if (collision.gameObject.CompareTag("EnemyAttack") &&
        //        ((Input.GetAxis("Horizontal") == 0 ||
        //        ((Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == true) || (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == false))))) //isso aqui � desnecess�rio
        //                                                                                                                                                 //pq � s� voltar pra falso depois de
        //                                                                                                                                                 //um tempo
        //{
        //    isDefending = false;
        //}

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //isDefending = false; //isso aqui � um problema, pq ele que t� deixando a vari�vel em falso antes do
        //dano mitigado acontecer, preciso fazer a vari�vel mudar apenas depois que o player soltar os direcionais
    }

    void ActiveDefense()
    {
        if (Input.GetKeyDown(KeyCode.L)) //preciso pegar a dist�ncia do inimigo pro player aqui, pra detectar que t�o de frente um pro outro ou fazer um escudo igual do smash
        {

            isDefending = true;

        } else if (Input.GetKeyUp(KeyCode.L)) 
        { 
            isDefending = false;
        }
    }

    void Defending(GameObject coll)
    {
        if (coll.gameObject.CompareTag("EnemyAttack") && isDefending == true)
            //(Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
            //Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false))
        {
            //isDefending = true;

            weight.velocity = Vector2.zero;

            weight.GetComponent<Rigidbody2D>().IsSleeping();

            //tenho que dar um jeito de voltar a isDefending pra false caso ele n�o continue segurando a dire��o das costas

            //while (isDefending == true) {


            //    if (((Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
            //          Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) && coll.gameObject.CompareTag("EnemyAttack"))
            //    {
            //        Defending(coll);

            //    }

        }
        else if (coll.gameObject.CompareTag("EnemyAttack") && isDefending != false)
            //((Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Horizontal") > 0.1) && walk.isFacingRight == true ||
            //((Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Horizontal") < -0.1) && walk.isFacingRight == false)))
        {
            //isDefending = false;
            weight.GetComponent<Rigidbody2D>().IsAwake();
            Debug.Log("Defendemos");
        }
        //StartCoroutine(DefenseEndCo());
    }

}
