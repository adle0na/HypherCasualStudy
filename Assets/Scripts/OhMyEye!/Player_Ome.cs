using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ome: MonoBehaviour
{
    [SerializeField]
    private GameController_Ome     _gameController;
    [SerializeField]
    private GameObject             playerDieEffect;
    [SerializeField]
    private PlayerTrailSpawner_Ome _playerTrailSpawner;
    [SerializeField]
    private float                  moveSpeed = 5;
    [SerializeField]
    private float                  jumpForce = 15;
    
    private Rigidbody2D _rb2D;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource        = GetComponent<AudioSource>();
        _rb2D               = GetComponent<Rigidbody2D>();
        _rb2D.isKinematic   = true;
    }

    private IEnumerator Start()
    {
        float originY    = transform.position.y;
        float deltaY     = 0.5f;
        float moveSpeedY = 2;

        while (true)
        {
            float y = originY + deltaY * Mathf.Sin(Time.time * moveSpeedY);
            transform.position = new Vector2(transform.position.x, y);

            yield return null;
        }
    }

    public void GameStart()
    {
        _rb2D.isKinematic = false;
        _rb2D.velocity    = new Vector2(moveSpeed, jumpForce);
        
        StopCoroutine(nameof(Start));
        StartCoroutine(nameof(UpdateInput));
    }
    
    private IEnumerator UpdateInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                JumpTo();
                
                _playerTrailSpawner.OnSpawns();
            }

            yield return null;
        }
    }

    private void ReverseXDir()
    {
        float x = -Mathf.Sign(_rb2D.velocity.x);
        _rb2D.velocity = new Vector2(x * moveSpeed, _rb2D.velocity.y);
    }

    private void JumpTo()
    {
        _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            ReverseXDir();
            
            _gameController.CollisionWithWall();
            
            _audioSource.Play();
        }
        else if (collision.CompareTag("Spike"))
        {
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            
            _gameController.GameOver();
            
            
            
            gameObject.SetActive(false);
        }
    }
}
