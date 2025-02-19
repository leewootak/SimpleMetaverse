using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endUI, startUI;
    public Button retryButton, exitButton;
    public TextMeshProUGUI scoreText;

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
    }

    public void SetEnd()
    {
        endUI.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}