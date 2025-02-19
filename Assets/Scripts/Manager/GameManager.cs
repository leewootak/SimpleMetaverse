using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // �ڽ��� ����

    public static GameManager Instance { get { return gameManager; } } // ������ ������ �������� ������Ƽ

    int currentScore = 0;
    int bestScore = 0;
    public int BestScore { get => bestScore; }

    private const string BestScoreKey = "BestScore";

    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        uiManager.UpdateScore(0);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        uiManager.SetStart();
    }

    public void GameOver()
    {
        Time.timeScale = 0.4f;
        uiManager.SetEnd();
        SaveScore();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� ���� �̸����� �ٽ� ���� �ε�
        Debug.Log($" ���� �ְ�����: {bestScore}");
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);
    }

    public void SaveScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            uiManager.Result(currentScore, bestScore);
            Debug.Log($"�ְ����� ����: {currentScore}");
        }
        else
        {
            uiManager.Result(currentScore, bestScore);
        }
    }
}