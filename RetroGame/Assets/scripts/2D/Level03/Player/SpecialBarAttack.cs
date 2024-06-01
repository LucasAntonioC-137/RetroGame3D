using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialBarAttack : MonoBehaviour
{
    private Life spBar;
    public Transform spInstance;
    public GameObject bltStrikePrefab;
    public float spSpeed;
    public bool spIsActive;
    private PlayerWalk facingDir;

    void Start()
    {
        spBar = GameObject.Find("Player").GetComponent<Life> ();
        facingDir = GameObject.Find("Player").GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Barra de especial atual: " + spBar.specialBar);
        }

        
    }

    private void FixedUpdate()
    {
        SpecialATK();
    }

    void SpecialATK()
    {
        //specBar = spBar.specialBar;

        if (spBar.special >= 100 && Input.GetKey(KeyCode.R)) //lembrar que o special é no R
        {

            spBar.special = 0;
            Debug.Log("Ativamos o special!");
            spIsActive = true; //pegar o script de movimentação do player e desativar a movimentação, aqui ou lá

            GameObject bullet = Instantiate(bltStrikePrefab, spInstance.position, transform.rotation);

            if(facingDir.isFacingRight == true)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(spSpeed, 0);
            }else// if (facingDir.isFacingRight == false) 
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-spSpeed, 0);
            }

            Debug.Log("Barra de especial do arrombado: " +  spBar.specialBar);
        }
    }
}
