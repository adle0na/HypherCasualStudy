using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;    // 플레이어 이동 제어를 위한 Movement
    private float    limitDeathY; // 플레이어가 사망하는 최소 Y높이 값

    private void Awake()
    {
        movement = GetComponent<Movement>();
        // 최초 이동방향을 오른쪽으로 설정
        movement.MoveTo(Vector3.right);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 현재 이동방향이 Vector3.forward(0, 0, 1)이면 이동방향을 Vector3.right(1, 0, 0)으로 설정
            // 현재 이동방향이 Vector3.right(1, 0, 0)이면 이동방향을 Vector3.forward(0, 0, 1)으로 설정
            Vector3 direction = movement.MoveDirection == Vector3.forward ? Vector3.right : Vector3.forward;
            movement.MoveTo(direction);
        }

        if (transform.position.y < limitDeathY)
        {
            Debug.Log("PlayerDie");
        }
    }
}
