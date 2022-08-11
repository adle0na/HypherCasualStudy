using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoDestroy_Wav : MonoBehaviour
{
    [SerializeField]
    private float destoryTime;

    private void Awake()
    {
        Destroy(gameObject, destoryTime);
    }
}
