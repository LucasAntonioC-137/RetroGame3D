using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    #region Movimentação do jogador
     [SerializeField]
    private int velocity;

    [SerializeField]
    private float minimumDistance;
    
    [SerializeField]
    Rigidbody2D rig;

    [SerializeField]
    private SpriteRenderer spriteRen;

    //[SerializeField]
    //private Animator anim;

    #endregion Movimentação do jogador
    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private float visionRadius;

    private void Update() {
        SearchPlayer();
        if(target != null){
            Move();
        } else { //não há um alvo
            StopMoving();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, visionRadius);   
    }

    private void SearchPlayer(){
        Collider2D colisor = Physics2D.OverlapCircle(transform.position, visionRadius);
        if (colisor != null && gameObject.tag == "player"){
            target = colisor.transform;
        }
    }

    private void Move()
    {
        Vector2 targetPosition = target.position;
        Vector2 actualPosition = transform.position;

        float distance = Vector2.Distance(actualPosition, targetPosition);
        if(distance >= minimumDistance){
            // Movimento do inimigo
            Vector2 direction = targetPosition - actualPosition;
            direction = direction.normalized;

            rig.velocity = (velocity * direction);

            if(rig.velocity.x > 0) { //direita
                spriteRen.flipX = false;
            } else if (rig.velocity.y < 0){ //esquerda
                spriteRen.flipX = true;
            }
            //this.animator.SetBool("moving", true); //lembrar de atribuir o inimigo no inspetor
        } else {
            StopMoving();
        }
    }

    private void StopMoving(){
        rig.velocity = Vector2.zero; //(0, 0)
    }
}
