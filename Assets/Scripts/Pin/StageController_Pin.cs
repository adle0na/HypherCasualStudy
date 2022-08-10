using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageController_Pin : MonoBehaviour
{
    [SerializeField]
    private PinSpawner_Pin _pinSpawner;
    [SerializeField]
    private Camera         mainCamera;
    [SerializeField]
    private Rotator_Pin    rotatorTarget;
    [SerializeField]
    private Rotator_Pin    rotatorIndexPanel;
    [SerializeField]
    private MainMenuUI_Pin _mainMenuUI;
    [SerializeField]
    private int            throwablePinCount;
    [SerializeField]
    private int            stuckPinCount;

    private Vector3        firstTPinPosition = Vector3.down * 2;
    public float           TPinDistance { set; get; } = 1;
    public bool            IsGameOver   { set; get; } = false;
    public bool            IsGameStart  { set; get; } = false;
    
    private Color gameOverColor = new Color(0.4f, 0.1f, 0.1f);
    private Color clearColor    = new Color(0, 0.5f, 0.25f);

    [Header("Sound")]
    [SerializeField]
    private AudioClip   audioGameOver;
    [SerializeField]
    private AudioClip   audioGameClear;

    private AudioSource _audioSource;
    
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _pinSpawner.Setup();

        for (int i = 0; i < throwablePinCount; ++i)
        {
            _pinSpawner.SpawnThrowablePin(firstTPinPosition + Vector3.down * TPinDistance * i, throwablePinCount - i);
        }

        for (int i = 0; i < stuckPinCount; ++i)
        {
            float angle = (360 / stuckPinCount) * i;

            _pinSpawner.SpawnStuckPin(angle, throwablePinCount + 1 + i);
        }
    }

    public void DecreaseThrowablePin()
    {
        throwablePinCount--;

        if (throwablePinCount == 0)
            StartCoroutine("GameClear");
    }

    public void GameOver()
    {
        IsGameOver = true;

        mainCamera.backgroundColor = gameOverColor;

        rotatorTarget.Stop();

        _audioSource.clip = audioGameOver;
        _audioSource.Play();

        StartCoroutine("StageExit", 0.5f);
    }

    private IEnumerator GameClear()
    {
        yield return new WaitForSeconds(0.1f);

        if (IsGameOver == true)
            yield break;

        mainCamera.backgroundColor = clearColor;

        rotatorTarget.RotateFast();
        rotatorIndexPanel.RotateFast();

        int index = PlayerPrefs.GetInt("StageLevel");
        PlayerPrefs.SetInt("StageLevel", index+1);
        
        _audioSource.clip = audioGameClear;
        _audioSource.Play();
        
        StartCoroutine("StageExit", 1);
    }

    private IEnumerator StageExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        _mainMenuUI.StageExit();
    }
}
