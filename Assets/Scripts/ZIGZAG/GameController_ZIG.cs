using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController_ZIG : MonoBehaviour
{
    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect_ZIG[]    fadeGameStart;
    [SerializeField]
    private GameObject      panelGameStart;
    [SerializeField]
    private TextMeshProUGUI textGameStartBestScore;

    [Header("InGame UI")]
    [SerializeField]
    private TextMeshProUGUI textInGameScore;


    [Header("GameOver UI")]
    [SerializeField]
    private GameObject      panelGameOver;
    [SerializeField]
    private TextMeshProUGUI textGameOverScore;
    [SerializeField]
    private TextMeshProUGUI textGameOverBestScore;
    [SerializeField]
    private float           timeStopTime;
    [SerializeField]
    private int             currentScore = 0;
    
    public bool IsGameStart { private set; get; } = false;
    public bool IsGameOver  { private set; get; } = false;

    private IEnumerator Start()
    {
        Time.timeScale = 1;

        int bestScore = PlayerPrefs.GetInt("BestScore");
        textGameStartBestScore.text = bestScore.ToString();
        
        for (int i = 0; i < fadeGameStart.Length; ++i)
            fadeGameStart[i].FadeIn();

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
                
                IsGameStart = true;
                
                yield break;
            }

            yield return null;
        }
    }

    public void GameStart()
    {
        panelGameStart.SetActive(false);
        
        textInGameScore.gameObject.SetActive(true);
    }

    public void IncreaseScore(int score = 1)
    {
        currentScore += score;

        textInGameScore.text = currentScore.ToString();
    }

    public void GameOver()
    {
        IsGameOver = true;

        textGameOverScore.text = currentScore.ToString();
        
        textInGameScore.gameObject.SetActive(false);
        
        panelGameOver.SetActive(true);

        int bestScore = PlayerPrefs.GetInt("BestScore");

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            textGameOverBestScore.text = currentScore.ToString();
        }
        else
            textGameOverBestScore.text = bestScore.ToString();
        
        StartCoroutine("SlowAndStopTime");
    }

    private IEnumerator SlowAndStopTime()
    {
        float current = 0;
        float percent = 0;

        Time.timeScale = 0.5f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / timeStopTime;
            
            yield return null;
        }

        Time.timeScale = 0;
    }
}
