using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public int life = 10;
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Animator animator;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {  
        Move();
        //Shooting
        // if(Input.GetButtonDown("attack")){
        //     StartCoroutine(AttackCo());
        // }
    }

    // private IEnumerator AttackCo()
    // {
    //     animator.SetBool("attacking", true);
    //     //state machine ficaria aqui
    //     yield return null;
    //     animator.SetBool("attacking", false);
    //     yield return new WaitForSeconds(.33f);
    //     //state machine de andar
    // }

    void Move(){

        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
    
    // void Shoot(){
    //     if(Input.GetKeyDown(KeyCode.Space)){
            
    //         Debug.Log("atirou");
    //         }
    //     }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "GameOver"){
        
           // AudioController.current.PlayMusic(AudioController.current.deathSFX);
            EnvironmentController.instance.ShowGameOver();
            Destroy(gameObject);
       }

       if(collision.gameObject.tag == "Enemy"){
            life -= 2;
            Debug.Log(life);
       }

       if(life <= 0){
            // animaçao
            // AudioController.current.PlayMusic(AudioController.current.deathSFX);
            gameObject.SetActive(false);
            EnvironmentController.instance.ShowGameOver();
            //vamos ver se ponho destroy depois;
       }
    }

}

    
