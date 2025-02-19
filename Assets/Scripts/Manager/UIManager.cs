using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endUI, startUI;
    public Button retryButton, exitButton;
    public TextMeshProUGUI scoreText, curScore, bestScore;

    public void Start()
    {
        if (retryButton == null)
        {
            Debug.LogError("retry button is null");
        }

        if (exitButton == null)
        {
            Debug.LogError("exit button is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        startUI.gameObject.SetActive(true);
        endUI.gameObject.SetActive(false);
    }

    public void SetStart()
    {
        startUI.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void SetEnd()
    {
        endUI.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void Result(int score, int best)
    {
        curScore.text = score.ToString();
        bestScore.text = best.ToString();
    }
}