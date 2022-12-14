using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageController_Wav : MonoBehaviour
{
    [SerializeField]
    private CameraController_Wav _cameraController;
    [SerializeField]
    private GameObject           textTitle;
    [SerializeField]
    private GameObject           textTaptoPlay;
    [SerializeField]
    private TextMeshProUGUI      textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI      textBestScore;
    [SerializeField]
    private GameObject           buttonContinue;
    [SerializeField]
    private GameObject           textScoreText;
    [SerializeField]
    private GameObject           pauseButton;
    
    private int                  currentScore = 0;
    private int                  bestScore    = 0;
    public bool        IsGameOver { private set; get; } = false;
    
    private IEnumerator Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = $"<size=50>BEST</size>\n<size=100>{bestScore}</size>";
        
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();

                yield break;
            }
            yield return null;
        }
    }

    private void GameStart()
    {
        pauseButton.SetActive(true);
        textTitle.SetActive(false);
        textTaptoPlay.SetActive(false);
        
        textCurrentScore.gameObject.SetActive(true);
    }
    
    public void GameOver()
    {
        ShakeCamera_Wav.Instance.OnShakeCamera(0.5f, 0.2f);
        
        IsGameOver = true;

        if (currentScore == bestScore)
            PlayerPrefs.SetInt("BestScore", currentScore);
        
        pauseButton.SetActive(false);
        buttonContinue.SetActive(true);
        textScoreText.SetActive(true);
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;

        textCurrentScore.text = currentScore.ToString();

        _cameraController.ChangeBackgroundColor();
        
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            textBestScore.text = $"<size=50>BEST</size>\n<size=100>{currentScore}</size>";
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
