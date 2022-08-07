using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;
    private Movement       _movement;  
    private float          limitDeathY; 

    private void Awake()
    {
        _movement = GetComponent<Movement>();

        limitDeathY = transform.position.y - transform.localScale.y * 2;
    }
    
    private IEnumerator Start()
    {
        while (true)
        {
            if (_gameController.IsGameStart == true)
            {
                _movement.MoveTo(Vector3.right);
                
                yield break;
            }

            yield return null;
        }
    }
    
    private void Update()
    {
        if (_gameController.IsGameOver == true) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = _movement.MoveDirection == Vector3.forward ? Vector3.right : Vector3.forward;
            _movement.MoveTo(direction);
            
            _gameController.IncreaseScore();
        }

        if (transform.position.y < limitDeathY)
        {
            _gameController.GameOver();
        }
    }
}
