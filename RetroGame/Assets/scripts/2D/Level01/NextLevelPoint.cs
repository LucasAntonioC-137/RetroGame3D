using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelPoint : MonoBehaviour
{
    public string sceneName;
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioController.current.PlayMusic(AudioController.current.bgm);
            SceneManager.LoadScene(sceneName);
        }
    }

}
