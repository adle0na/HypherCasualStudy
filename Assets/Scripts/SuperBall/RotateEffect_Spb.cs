using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffect_Spb : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}
