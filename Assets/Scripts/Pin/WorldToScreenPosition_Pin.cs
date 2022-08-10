using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenPosition_Pin : MonoBehaviour
{
    [SerializeField]
    private Vector3       distance = Vector3.zero;

    private Transform     targetTransform;
    private RectTransform _rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition  = Camera.main.WorldToScreenPoint(targetTransform.position);
        _rectTransform.position = screenPosition + distance;
    }
}
