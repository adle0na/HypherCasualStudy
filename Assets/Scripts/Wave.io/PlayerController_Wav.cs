using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Wav : MonoBehaviour
{
    [SerializeField]
    private StageController_Wav   _stageController;

    [SerializeField]
    private GameObject            playerDieEffect;
    private Movement2D_Wav        _movement;
    
    private bool                  isClicked;
    private bool                  isGameStart = false;
    private AudioSource           _audioSource;
    
    [SerializeField]
    private AudioClip             _itemGetSound;
    [SerializeField]
    private AudioClip             _DieSound;

    private PlayerVisibleMode_Wav _visibleMode;
    private SpriteRenderer        _renderer;

    private void Awake()
    {
        _renderer        = gameObject.GetComponent<SpriteRenderer>();
        _visibleMode     = GetComponent<PlayerVisibleMode_Wav>();
        _movement        = GetComponent<Movement2D_Wav>();
        _audioSource     = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (_stageController.IsGameOver == true) return;
        
        _movement.MoveToX();
        
        if(isGameStart)
            _visibleMode.UpdateVisibleMode(!isClicked);
        
        if (Input.GetMouseButton(0))
        {
            isGameStart = true;
            isClicked = true;
            _movement.MoveToY();
            _renderer.color = new Color32(255, 255,255, 255);
        }
        else
        {
            if (!_visibleMode.IsCoolDown)
            {
                isClicked = false;
                _renderer.color = new Color32(255, 255,255, 100);
            }
            else
            {
                _renderer.color = new Color32(255, 255,255, 255);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item"))
        {
            _audioSource.clip = _itemGetSound;
            _audioSource.Play();
            _stageController.IncreaseScore(1);
            
            collision.GetComponent<Item_Wav>().Exit();
        }
        else if (collision.tag.Equals("Obstacle") && isClicked)
        {
            GameOver();
        }
        else if (collision.tag.Equals("Obstacle") && _visibleMode.IsCoolDown)
        {
            GameOver();
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle") && isClicked)
            GameOver();
        else if (collision.tag.Equals("Obstacle") && _visibleMode.IsCoolDown)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            
        Destroy(GetComponent<Rigidbody2D>());
            
        _stageController.GameOver();
        _visibleMode.Deactivate();
        
        _audioSource.clip = _DieSound;
        _audioSource.Play();
    }

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color32(255, 255,255, 100);
    }
}
