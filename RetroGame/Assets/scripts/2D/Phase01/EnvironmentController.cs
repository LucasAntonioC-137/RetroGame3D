using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour
{
    //game controller    
    public int playerScore;
    public Text scoreText;

    public static EnvironmentController instance;

    void Start()
    {
        instance = this;
    }

     public void UpdateScoreText()
    {
        scoreText.text = playerScore.ToString();
    }
}
