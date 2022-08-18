using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Ome : MonoBehaviour
{
    [SerializeField]
    private SpikeSpawner_Ome[] _spikeSpawners;
    [SerializeField]
    private Player_Ome         player;
    private UIController_Ome   _uiController;
    private RandomColor_Ome    _randomColor;
    private int                currentSpawn = 0;
    private int                currentScore = 0;

    private void Awake()
    {
        _uiController = GetComponent<UIController_Ome>();
        _randomColor  = GetComponent<RandomColor_Ome>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.GameStart();
                _uiController.GameStart();

                yield break;
            }

            yield return null;
        }
    }

    public void CollisionWithWall()
    {
        UpdateSpikes();

        currentScore++;
        _uiController.UpdateScore(currentScore);
        
        _randomColor.OnChange();
    }

    public void GameOver()
    {
        StartCoroutine(nameof(GameOverProcess));
        _uiController.GameOver();
    }
    
    private void UpdateSpikes()
    {
        _spikeSpawners[currentSpawn].ActivateAll();

        currentSpawn = (currentSpawn + 1) % _spikeSpawners.Length;
        
        _spikeSpawners[currentSpawn].DeactivateAll();
    }

    private IEnumerator GameOverProcess()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", currentScore);

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            yield return null;
        }

    }
}
