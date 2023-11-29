using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnvironmentController : MonoBehaviour
{
    //game controller    
    public int playerScore;
    public Text scoreText;
    public GameObject gameOver;

    public static EnvironmentController instance;

    void Start()
    {
        instance = this;
    }

     public void UpdateScoreText()
    {
        scoreText.text = playerScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
