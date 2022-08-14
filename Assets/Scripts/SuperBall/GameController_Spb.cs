using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Spb : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner_Spb _platformSpawner;
    [SerializeField]
    private UIController_Spb    _uiController;

    [Header("SFX")]
    [SerializeField]
    private AudioClip           gameOverClip;
    [SerializeField]
    private AudioClip           gameClearClip;


    [Header("VFX")]
    [SerializeField]
    private GameObject          gameOverEffect;
    [SerializeField]
    private GameObject          gameClearEffect;
    
    private RandomColor_Spb     _randomColorSpb;
    private AudioSource         _audioSource;

    private int brokePlatformCount = 0;
    private int totalPlatformCount;
    private int currentScore = 0;
    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        _uiController.CurrentScore = currentScore;
        
        totalPlatformCount = _platformSpawner.SpawnPlatfroms();

        _randomColorSpb = GetComponent<RandomColor_Spb>();
        _randomColorSpb.ColorHSV();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) ||
                PlayerPrefs.GetInt("DeactivateMain") == 1)
            {
                GameStart();
                
                yield break;
            }

            yield return null;
        }
    }

    private void GameStart()
    {
        IsGamePlay = true;
        
        _uiController.GameStart();
    }

    public void OnCollisionWithPlatform(int addedScore = 1)
    {
        brokePlatformCount++;
        _uiController.LevelProgressBar = (float) brokePlatformCount / (float) totalPlatformCount;

        currentScore += addedScore;
        _uiController.CurrentScore = currentScore;
    }

    public void GameOver(Vector3 position)
    {
        IsGamePlay = false;

        _audioSource.clip = gameOverClip;
        _audioSource.Play();
        gameOverEffect.transform.position = position;
        gameOverEffect.SetActive(true);

        UpdateHighScore();
        
        _uiController.GameOver(currentScore);
        
        PlayerPrefs.SetInt("Level", 0);
        
        PlayerPrefs.SetInt("CurrentScore", 0);

        StartCoroutine(nameof(SceneLoadToOnClick));
    }

    public void GameClear()
    {
        IsGamePlay = false;

        _audioSource.clip = gameClearClip;
        _audioSource.Play();
        gameClearEffect.SetActive(true);
        
        UpdateHighScore();
        
        _uiController.GameClear();
        
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        
        PlayerPrefs.SetInt("CurrentScore", currentScore);

        StartCoroutine(nameof(SceneLoadToOnClick));
    }
    
    private void UpdateHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }

    private IEnumerator SceneLoadToOnClick()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                UnityEngine.SceneManagement.SceneManager.LoadScene("SuperBall");

            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    [ContextMenu("Reset All PlayerPrefs")]
    private void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
