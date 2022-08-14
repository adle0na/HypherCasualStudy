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

    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        nextlevel.text    = (PlayerPrefs.GetInt("Level") + 2).ToString();
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
    }
    
    public float LevelProgressBar { set => levelProgressBar.fillAmount = value; }

    public int CurrentScore { set => currentScore.text = value.ToString(); }

}
