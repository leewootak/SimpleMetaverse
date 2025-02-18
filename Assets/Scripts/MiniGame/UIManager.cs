using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panel;
    public Button retryButton, exitButton;
    public TextMeshProUGUI scoreText

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

        panel.gameObject.SetActive(false);
    }

    public void SetEnd()
    {
        panel.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}