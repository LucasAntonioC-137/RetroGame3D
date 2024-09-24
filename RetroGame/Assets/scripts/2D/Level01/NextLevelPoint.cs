using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NextLevelPoint : MonoBehaviour
{
    public string sceneName;
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.AddListener(NextLevel);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            //AudioController.current.PlayMusic(AudioController.current.bgm);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
