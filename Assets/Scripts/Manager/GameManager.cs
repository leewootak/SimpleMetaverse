using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // �ڽ��� ����

    // �ܺο��� GameManager �ν��Ͻ� ����
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0;
    int bestScore = 0;

    // �ܺο��� �ְ� ���� ����
    public int BestScore { get => bestScore; }

    // �ְ� ���� ���� �� ��� Ű
    private const string BestScoreKey = "BestScore";

    // UIManager �ν��Ͻ� ����
    UIManager uiManager;

    // �ܺο��� UIManager �ν��Ͻ��� ���ٿ�
    public UIManager UIManager { get { return uiManager; } }

    // �ʱ�ȭ
    private void Awake()
    {        
        gameManager = this; // �̱��� �ν��Ͻ��� ���� ��ü �Ҵ�
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        // ����� �ְ� ������ �ҷ���, ������ 0
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        // �ʱ� ���� ������Ʈ
        uiManager.UpdateScore(0);
    }

    // ���� ����
    public void StartGame()
    {
        Time.timeScale = 1;
        uiManager.SetStart();
    }

    // ���� ����
    public void GameOver()
    {
        Time.timeScale = 0.4f;
        uiManager.SetEnd();
        SaveScore();
    }

    // �����
    public void RetryGame()
    {
        // ���� ���� ���� �̸����� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ������
    public void ExitGame()
    {
        Time.timeScale = 1;
        // ���� �� �ε�
        SceneManager.LoadScene("MainScene");
    }

    // ���� �߰�
    public void AddScore(int score)
    {
        currentScore += score;
        // ������Ʈ�� ������ UI�� �ݿ�
        uiManager.UpdateScore(currentScore);
    }

    // ���� ���� ���� �� ��
    public void SaveScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            // PlayerPrefs�� �ְ� ���� ���� ����
            PlayerPrefs.SetInt(BestScoreKey, bestScore);

            // ��� ȭ�鿡 ���� ǥ��
            uiManager.Result(currentScore, bestScore);
        }
        else
        {
            uiManager.Result(currentScore, bestScore);
        }
    }
}
