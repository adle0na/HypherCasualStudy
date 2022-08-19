using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic_Wav : MonoBehaviour
{
    private static BackGroundMusic_Wav _backGroundMusic;

    private void Awake()
    {
        if (_backGroundMusic == null)
        {
            _backGroundMusic = this;
            DontDestroyOnLoad(_backGroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
