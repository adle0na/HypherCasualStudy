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
    
    private RandomColor_Spb     _randomColorSpb;

    private int brokePlatformCount = 0;
    private int totalPlatformCount;
    private int currentScore = 0;
    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        totalPlatformCount = _platformSpawner.SpawnPlatfroms();

        _randomColorSpb = GetComponent<RandomColor_Spb>();
        _randomColorSpb.ColorHSV();
    }

    private IEnumerator Start()
    {
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
}
