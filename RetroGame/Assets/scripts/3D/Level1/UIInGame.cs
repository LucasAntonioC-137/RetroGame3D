using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public Text scoreText;
    public Image lifeBar;
    public Image redBar;
    public int currentHealth;
    private PlayerHealth player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        this.scoreText.text = PlayerStats.instance.score.ToString("D9");
        SetHealth(player.currentHealth);
    }

    public void SetHealth(int amount)
    {
        currentHealth = Mathf.Clamp(amount, 0, player.maxHealth);

        Vector3 lifebarScale = lifeBar.rectTransform.localScale;
        lifebarScale.x = (float)currentHealth / player.maxHealth;
        lifeBar.rectTransform.localScale = lifebarScale;
        StartCoroutine(DecreasingRedBar(lifebarScale));
    }

    IEnumerator DecreasingRedBar(Vector3 newScale)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 redBarScale = redBar.transform.localScale;
        while (redBar.transform.localScale.x > newScale.x)
        {
            redBarScale.x -= Time.deltaTime * 0.25f;
            redBar.transform.localScale = redBarScale;
        }
    }

}
