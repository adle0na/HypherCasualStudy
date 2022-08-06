using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float   moveSpeed;      // 이동속도
    [SerializeField]
    private Vector3 moveDirection;  // 이동방향

    // 외부에서 이동방향을 확인 할 수 있도록 Get 프로퍼티 선언
    public Vector3 MoveDirection => moveDirection;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
