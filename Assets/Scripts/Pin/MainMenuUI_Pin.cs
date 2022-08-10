using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI_Pin : MonoBehaviour
{
    [SerializeField]
    private StageController_Pin    _stageControllerPin;
    [SerializeField]
    private RectTransformMover_Pin menuPanel;
    [SerializeField]
    private TextMeshProUGUI        textLevelInMenu;
    [SerializeField]
    private TextMeshProUGUI        textLevelInGame;

    private Vector3                inactivePosition = Vector3.left * 1080;
    private Vector3                activePosition   = Vector3.zero;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInMenu.text = $"Level {(index + 1)}";
    }

    public void ButtonClickEventStart()
    {
        menuPanel.MoveTo(AfterStartEvent, inactivePosition);
        
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInGame.text = $"{(index + 1)}";
    }

    private void AfterStartEvent()
    {
        _stageControllerPin.IsGameStart = true;
    }
    
    public void ButtonClickEventReset()
    {
        PlayerPrefs.SetInt("StageLevel", 0);
    }

    public void ButtonClickEventQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    public void StageExit()
    {
        menuPanel.MoveTo(AfterStageExitEvent, activePosition);
        
        int index = PlayerPrefs.GetInt("StageLevel");

        textLevelInMenu.text = $"Level {(index + 1)}";
    }

    private void AfterStageExitEvent()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        if (index == SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("StageLevel", 0);
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(index);
    }


}
