using Level3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int maxLife = 100;
    public int currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //NÃO ESTOU CONSEGUINDO FAZER O PLAYER TOMAR DANO, ver o vídeo novamente
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            TakeDamage();
            if (currentLife <= 0) { Die(); }
        }

        void TakeDamage()
        {
            currentLife -= 10;
            Debug.Log("Current life: " + currentLife);
        }

        void Die()
        {
            Debug.Log("Player Died");
        }
    }
}