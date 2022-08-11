using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Stk : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner_Stk      _cubeSpawner;
    [SerializeField]
    private CameraController_Stk _cameraController;
    [SerializeField]
    private UIController_Stk     _uiController;

    private bool                 isGameStart  = false;
    private int                  currentScore = 0;
        
    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isGameStart == false)
                {
                    isGameStart = true;
                    _uiController.GameStart();
                }
                
                if (_cubeSpawner.CurrentCube != null)
                {
                    bool isGameOver = _cubeSpawner.CurrentCube.Arrangement();
                    if (isGameOver == true)
                    {
                        _cameraController.GameOverAnimation(_cubeSpawner.LastCube.position.y, OnGameOver);
                        
                        yield break;
                    }
                    currentScore++;
                    _uiController.UpdateScore(currentScore);
                }

                _cameraController.MoveOneStep();

                _cubeSpawner.SpawnCube();
            }

            yield return null;
        }
    }

    private void OnGameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            _uiController.GameOver(true);
        }
        else
        {
            _uiController.GameOver(false);
        }

        StartCoroutine("AfterGameOver");
    }

    private IEnumerator AfterGameOver()
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                UnityEngine.SceneManagement.SceneManager.LoadScene("Stack");

            yield return null;
        }
    }
}
