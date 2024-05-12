using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public bool isDefending = false; //utilizar para desativar a possibilidade de movimenta��o na defesa
    private PlayerWalk walk;
    // Start is called before the first frame update
    private void Awake()
    {
        walk = GameObject.Find("Player").GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void GetDirection()
    //{
    //    movement = new Vector3(Input.GetAxis("Horizontal"))
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("EnemyAttack")) { Debug.Log("eu existo"); return; } //funcionou, agora por que n�o funciona quando pe�o pra
                                                                                                //testar com o player andando pra tr�s? Resposta: precisava instanciar o script
                                                                                                //no awake pra que o IF "soubesse" que a vari�vel isFacingRight existe
        if (collision.gameObject.CompareTag("EnemyAttack") && //n�o est� pegando o colisor por ele aparentemente "n�o estar instanciado"
        (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
        Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador t� andando pra tr�s && (FUNCIONANDO)
        {
            Debug.Log("Defendemos!");
            isDefending = true; //fazer durar uma quantidade espec�fica de frames
        }


        if (collision.gameObject.CompareTag("EnemyAttack")) { Debug.Log("mizera"); }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (collision.gameObject.CompareTag("EnemyAttack") &&
    //        (Input.GetAxis("Horizontal") < 0 && walk.isFacingRight == true ||
    //         Input.GetAxis("Horizontal") > 0 && walk.isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador t� andando pra tr�s && )
    //    {
    //        Debug.Log("Defendemos!");
    //        isDefending = true; //fazer durar uma quantidade espec�fica de frames
    //    }


    //    if (collision.gameObject.CompareTag("EnemyAttack")){ Debug.Log("mizera"); }
    //    //(movement.x < 0 && isFacingRight == true ||
    //    // movement.x > 0 && isFacingRight == false)) //Aqui preciso fazer ele detectar que o jogador t� andando pra tr�s && )
    //    //{
    //    //    Debug.Log("Defendemos!");
    //    //    isDefending = true; //fazer durar uma quantidade espec�fica de frames
    //    //}

    //}


}
