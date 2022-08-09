using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float   rotateSpeed = 50;
    [SerializeField]
    private Vector3 rotateAngle = Vector3.forward;

    private void Update()
    {
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime);
    }
}
