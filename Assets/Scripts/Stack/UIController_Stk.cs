using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController_Stk : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject      mainPannel;

    [Header("InGame")]
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;

    [Header("GameOver")]
    [SerializeField]
    private GameObject      textNewRecord;
    [SerializeField]
    private GameObject      imageCrown;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    [SerializeField]
    private TextMeshProUGUI textHighScoreText;
    [SerializeField]
    private GameObject      textTouchToRestart;


    public void GameStart()
    {
        mainPannel.SetActive(false);
        
        textCurrentScore.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        textCurrentScore.text = score.ToString();
    }

    public void GameOver(bool isNewRecord)
    {
        if (isNewRecord == true)
            textNewRecord.SetActive(true);
        else
        {
            imageCrown.SetActive(true);

            textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            textHighScore.gameObject.SetActive(true);
            textHighScoreText.gameObject.SetActive(true);
        }
        
        textTouchToRestart.SetActive(true);
    }

}
