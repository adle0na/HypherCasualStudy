using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Wav : MonoBehaviour
{
    [SerializeField]
    private StageController_Wav _stageController;

    [SerializeField]
    private GameObject          playerDieEffect;
    private Movement2D_Wav      _movement;


    private void Awake()
    {
        _movement        = GetComponent<Movement2D_Wav>();
    }

    private void FixedUpdate()
    {
        if (_stageController.IsGameOver == true) return;
        
        _movement.MoveToX();
        
        if(Input.GetMouseButton(0))
            _movement.MoveToY();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item"))
        {
            _stageController.IncreaseScore(1);

            collision.GetComponent<Item_Wav>().Exit();
        }
        else if (collision.tag.Equals("Obstacle"))
        {
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            
            Destroy(GetComponent<Rigidbody2D>());
            
            _stageController.GameOver();
        }
    }
}
