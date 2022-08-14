using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController_Spb : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private TextMeshProUGUI currentLevel;
    [SerializeField]
    private TextMeshProUGUI nextlevel;
    
    [Header("Main")]
    [SerializeField]
    private GameObject      mainPanel;

    [Header("InGame")]
    [SerializeField]
    private Image           levelProgressBar;
    [SerializeField]
    private TextMeshProUGUI currentScore;

    [Header("GameOver")]
    [SerializeField]
    private GameObject      gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI textHighScore;

    [Header("GameClear")]
    [SerializeField]
    private GameObject      gameClearPanel;
    [SerializeField]
    private TextMeshProUGUI textLevelCompleted;

    

    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        nextlevel.text    = (PlayerPrefs.GetInt("Level") + 2).ToString();
        
        if (PlayerPrefs.GetInt("DeactivateMain") == 0)
            mainPanel.SetActive(true);
        else
            mainPanel.SetActive(false);
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
    }

    public void GameOver(int currentScore)
    {
        textCurrentScore.text = $"Score\n{currentScore}";
        textHighScore.text = $"High Score\n{PlayerPrefs.GetInt("HighScore")}";
        
        gameOverPanel.SetActive(true);
        
        PlayerPrefs.SetInt("DeactivateMain", 0);
    }

    public void GameClear()
    {
        textLevelCompleted.text = $"Level {PlayerPrefs.GetInt("Level") + 1}\nComplete!";
        
        gameClearPanel.SetActive(true);
        
        PlayerPrefs.SetInt("DeactivateMain", 1);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("DeactivateMain", 0);
    }

    public float LevelProgressBar { set => levelProgressBar.fillAmount = value; }

    public int CurrentScore { set => currentScore.text = value.ToString(); }

}
