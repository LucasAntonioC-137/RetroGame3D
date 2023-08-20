using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float startTime;
    private float currentTime;

    public GUIStyle timerStyle;

    void Start()
    {
        startTime = Time.time;
        timerStyle = new GUIStyle();
        timerStyle.fontSize = 40;
        timerStyle.normal.textColor = Color.white;
        timerStyle.normal.background = MakeTex(2, 2, new Color(0, 0, 1, 0.5f));
    }

    void Update()
    {
        currentTime = Time.time - startTime;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), "Time: " + currentTime.ToString("F2"), timerStyle);
    }
    Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = color;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
