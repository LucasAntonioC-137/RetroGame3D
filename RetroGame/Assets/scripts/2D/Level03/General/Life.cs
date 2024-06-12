using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour //, Damage
{
    public float life = 100;
    public float special = 25;
    public Image lifeBar;
    public Image specialBar;

    private void Update()
    {
        lifeBar.fillAmount = life / 200f;
        specialBar.fillAmount = special/ 100f;
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        //LifeBar.fillAmount = life / 100;

        special += damage * 2f;
        //Debug.Log("Vida: " + life + " Special bar: " + special);

        if (special >= 100)
        {
            special = 100;
        }

        if(life <= 0)
        {
            Debug.Log("Game over pra esse aqui");
        }
        
    }
}
