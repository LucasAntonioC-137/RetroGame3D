using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIendText : MonoBehaviour
{
    public float delay = 0.1f; // Atraso entre cada letra
    private string fullText;
    public TMP_Text textComponent;
    public TMP_Text textToDisplay; // O texto que voc� quer exibir
    public TMP_Text scoreText; // O texto que voc� quer exibir
    public float duration = 1.0f; // A dura��o da anima��o da pontua��o   

    public RectTransform imageToMove; // A imagem que voc� quer mover
    public Vector3 startPosition; // A posi��o inicial da imagem
    public Vector3 endPosition; // A posi��o final da imagem
    public float speed = 1.0f; // A velocidade da movimenta��o
    private bool shouldMove = false;
    private float progress = 0;

    public Button restartButton;

    void Start()
    {

        startPosition = imageToMove.localPosition;
        textToDisplay.color = new Color(textToDisplay.color.r, textToDisplay.color.g, textToDisplay.color.b, 0);
        fullText = textComponent.text; // Armazena o texto completo
        textComponent.text = ""; // Come�a com o texto vazio
        restartButton.onClick.AddListener(OnResetButtonPressed);
        StartCoroutine(TypeText());
    }
    void Update()
    {
        if (shouldMove)
        {
            // Mova a imagem de startPosition para endPosition
            progress += speed * Time.deltaTime;
            imageToMove.localPosition = Vector3.Lerp(startPosition, endPosition, progress);
            if (imageToMove.localPosition == endPosition)
            {
                textToDisplay.color = new Color(textToDisplay.color.r, textToDisplay.color.g, textToDisplay.color.b, 1);
                int score = PlayerPrefs.GetInt("Score"); // Recupera a pontua��o
                StartCoroutine(AnimateScore(score));
            }
        }
    }
    public void StartMoving()
    {
        shouldMove = true;
        progress = 0; // Reinicia o progresso quando come�a a mover
    }
    IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay);
        }
        StartMoving();
    }
    IEnumerator AnimateScore(int targetScore)
    {
        float elapsed = 0;
        int startScore = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            int score = Mathf.RoundToInt(Mathf.Lerp(startScore, targetScore, t));
            scoreText.text = score.ToString();

            yield return null;
        }

        // Garante que a pontua��o final seja exatamente a pontua��o alvo
        scoreText.text = targetScore.ToString();
    }
    void OnResetButtonPressed()
    {
        SceneManager.LoadScene("Nivel 1 - 3D");
    }

}
