using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        this.scoreText.text = PlayerStats.instance.score.ToString();
    }
}
