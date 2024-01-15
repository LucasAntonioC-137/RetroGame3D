using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int life;

    public PlayerState currentState;

    public float speed;
    public Rigidbody2D rbody;
    private Vector3 change;
    private Animator animator;

    void Start() {
        Application.targetFrameRate = 60;
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update(){

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
      
        if(currentState == PlayerState.walk)
        {
            UpddateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    void UpddateAnimationAndMove(){
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("Horizontal", change.x);
            animator.SetFloat("Vertical", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }
    

    void MoveCharacter(){
        // rbody.MovePosition(
        //     transform.position + change * speed * Time.deltaTime
        // );
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

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