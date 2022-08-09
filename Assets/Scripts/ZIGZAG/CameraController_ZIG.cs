using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_ZIG : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float     distance;

    private void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    private void LateUpdate()
    {
        // 타겟이 존재하지 않으면 실행 하지 않음
        if (target == null) return;
        // 카메라의 위치(position) 정보 갱신
        // target 위치를 기준으로 distance만큼 떨어져서 쫒아간다
        transform.position = target.position + transform.rotation * new Vector3(0, 0, -distance);
    }
}
