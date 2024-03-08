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
    public GameObject winingScreen;

    public bool isPaused;

    public static EnvironmentController instance;

    void Start()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
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

    public void ShowWinningScreen()
    {
        winingScreen.SetActive(true);
    }

    private void Pause()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
        } 
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
        }

    }
}
