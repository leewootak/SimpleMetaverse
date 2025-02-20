using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // 자신을 참조

    // 외부에서 GameManager 인스턴스 접근
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0;
    int bestScore = 0;

    // 외부에서 최고 점수 접근
    public int BestScore { get => bestScore; }

    // 최고 점수 저장 용 상수 키
    private const string BestScoreKey = "BestScore";

    // UIManager 인스턴스 참조
    UIManager uiManager;

    // 외부에서 UIManager 인스턴스에 접근용
    public UIManager UIManager { get { return uiManager; } }

    // 초기화
    private void Awake()
    {        
        gameManager = this; // 싱글톤 인스턴스로 현재 객체 할당
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        // 저장된 최고 점수를 불러옴, 없으면 0
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        // 초기 점수 업데이트
        uiManager.UpdateScore(0);
    }

    // 게임 시작
    public void StartGame()
    {
        Time.timeScale = 1;
        uiManager.SetStart();
    }

    // 게임 오버
    public void GameOver()
    {
        Time.timeScale = 0.4f;
        uiManager.SetEnd();
        SaveScore();
    }

    // 재시작
    public void RetryGame()
    {
        // 현재 켜진 씬의 이름으로 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 나가기
    public void ExitGame()
    {
        Time.timeScale = 1;
        // 메인 씬 로드
        SceneManager.LoadScene("MainScene");
    }

    // 점수 추가
    public void AddScore(int score)
    {
        currentScore += score;
        // 업데이트된 점수를 UI에 반영
        uiManager.UpdateScore(currentScore);
    }

    // 현재 점수 저장 및 비교
    public void SaveScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            // PlayerPrefs에 최고 점수 영구 저장
            PlayerPrefs.SetInt(BestScoreKey, bestScore);

            // 결과 화면에 점수 표시
            uiManager.Result(currentScore, bestScore);
        }
        else
        {
            uiManager.Result(currentScore, bestScore);
        }
    }
}
