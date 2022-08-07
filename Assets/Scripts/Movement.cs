using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float   moveSpeed;
    [SerializeField]
    private float   increaseAmount;
    [SerializeField]
    private float   increaseCycleTime;
    
    private Vector3 moveDirection;
    private float   rotateSpeed;
    
    public Vector3 MoveDirection => moveDirection;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseCycleTime);

            moveSpeed += increaseAmount;
        }
    }
    
    private void OnEnable()
    {
        rotateSpeed = 3f;
    }
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(1, 1, 0) * rotateSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
