using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController_Wav : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float     yOffset    = 5;
    [SerializeField]
    private float     smoothTime = 0.3f;

    private Vector3   velocity = Vector3.zero;

    private Camera    _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, yOffset, -10));
        targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void ChangeBackgroundColor()
    {
        float colorHue = Random.Range(0, 10);
        colorHue *= 0.1f;
        _mainCamera.backgroundColor = Color.HSVToRGB(colorHue, 0.6f, 0.8f);
    }
}
