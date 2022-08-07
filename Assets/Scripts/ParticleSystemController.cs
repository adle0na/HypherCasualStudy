using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _particle.Play();
    }

    private void Update()
    {
        if (_particle.isPlaying == false)
            gameObject.SetActive(false);
    }
}
