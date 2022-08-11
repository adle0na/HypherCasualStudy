using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D_Wav : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField]
    private float       xMoveSpeed = 2;
    [SerializeField]
    private float       xDelta = 2;
    private float       xStartPosition;

    [Header("Vertical Movement")] [SerializeField]
    private float       yMoveSpeed = 0.5f;
    private Rigidbody2D _rigid2D;
    
    private void Awake()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        
        xStartPosition = transform.position.x;
    }

    public void MoveToX()
    {
        float x = xStartPosition + xDelta * Mathf.Sin(Time.time * xMoveSpeed);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void MoveToY()
    {
        _rigid2D.AddForce(transform.up * yMoveSpeed, ForceMode2D.Impulse);
    }
}
