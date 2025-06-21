using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentScore = 0;

    public CanvasGroup fadePanel;
    public TMP_Text score;
    public float fadeDuration = 1f;
    public TMP_Text displayScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit the game when Escape is pressed
            GotoMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the game when R is pressed
            Failed();
        }
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        currentScore = 0;
        score.text = currentScore.ToString();
        StartCoroutine(FadeBackGround());
    }

    public void Failed()
    {
        StartCoroutine(UnFadeBackGround());

        score.text = currentScore.ToString();
    }

    public void UpdateScore()
    {
        currentScore ++;
        displayScore.text ="Score :"+ currentScore.ToString();
    }

    IEnumerator UnFadeBackGround()
    {
        float timer = 0f;
        fadePanel.interactable = true;
        while (timer < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 1f;
    }

    IEnumerator FadeBackGround()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 0f;
        fadePanel.interactable = false;
    }
}
