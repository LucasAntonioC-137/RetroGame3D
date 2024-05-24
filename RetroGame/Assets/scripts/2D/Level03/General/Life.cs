using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour //, Damage
{
    public float life = 100;
    public float specialBar = 25;
    public Image LifeBar;

    private void Update()
    {
        LifeBar.fillAmount = life / 100f;
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        //LifeBar.fillAmount = life / 100;

        specialBar += damage * 2f;
        Debug.Log("Vida: " + life + " Special bar: " + specialBar);

        if (specialBar >= 100)
        {
            specialBar = 100;
        }

        if(life <= 0)
        {
            Debug.Log("Game over pra esse aqui");
        }
        
    }
}
