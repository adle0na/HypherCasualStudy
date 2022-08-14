using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDeactivate_Spb : MonoBehaviour
{
    private new ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_particleSystem.isPlaying)
            gameObject.SetActive(false);
    }
}
