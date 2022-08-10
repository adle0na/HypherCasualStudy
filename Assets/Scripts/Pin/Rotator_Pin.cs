using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_Pin : MonoBehaviour
{
    [SerializeField]
    private StageController_Pin _stageControllerPin;
    [SerializeField]
    private float               rotateSpeed;
    [SerializeField]
    private float               maxRotateSpeed = 500;
    [SerializeField]
    private Vector3             rotateAngle = Vector3.forward;

    public void Stop()
    {
        rotateSpeed = 0;
    }

    public void RotateFast()
    {
        rotateSpeed = maxRotateSpeed;
    }
    private void Update()
    {
        if (_stageControllerPin.IsGameStart == false) return;
        
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime);
    }
}
