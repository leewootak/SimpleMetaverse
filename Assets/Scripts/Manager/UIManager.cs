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

    // 게임 시작 시 UI 상태 변경
    public void SetStart()
    {
        startUI.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    // 게임 종료 시 UI 상태 변경
    public void SetEnd()
    {
        endUI.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    // 실시간 점수 반영
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // 게임 결과
    public void Result(int score, int best)
    {
        curScore.text = score.ToString();
        bestScore.text = best.ToString();
    }
}