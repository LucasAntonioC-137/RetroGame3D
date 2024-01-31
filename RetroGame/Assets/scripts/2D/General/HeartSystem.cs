using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int life;
    public int maxLife;

    public Image[] heart;
    public Sprite full;
    public Sprite hollow;

    void Start()
    {

    }

    void Update()
    {
        Health();
    }

    void Health()
    {
        
        if(life > maxLife)
        {
            life = maxLife;
        }
        
        for(int i = 0; i < heart.Length; i++)
        {
            if(i < life)
            {
                heart[i].sprite = full;
            }
            else
            {
                heart[i].sprite = hollow;
            }

            if(i < maxLife)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GameOver"){
            // AudioController.current.PlayMusic(AudioController.current.deathSFX);
            EnvironmentController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Enemy"){
            life -= 1;
            Debug.Log(life);
        }

        if(life <=0 ){
            // animaçao
            // AudioController.current.PlayMusic(AudioController.current.deathSFX);
            gameObject.SetActive(false);
            EnvironmentController.instance.ShowGameOver();
        }
    }

}
