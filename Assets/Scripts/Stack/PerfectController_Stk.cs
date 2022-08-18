using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectController_Stk : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner_Stk _cubeSpawner;
    [SerializeField]
    private Transform       perfectEffect;
    [SerializeField]
    private Transform       perfectComboEffect;
    [SerializeField]
    private Transform       perfectRecoveryEffect;
    
    private AudioSource     _audioSource;

    [SerializeField]
    private int   recoveryCombo     = 5;
    private float perfectCorrection = 0.04f;
    private float addedSize         = 0.1f;
    private int   perfectCombo      = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public bool IsPerfect(float hangOver)
    {
        if (Mathf.Abs(hangOver) <= perfectCorrection)
        {
            EffectProcess();
            SFXProcess();
            
            perfectCombo++;

            return true;
        }
        else
        {
            perfectCombo = 0;
            
            return false;
        }
    }
    
    private void EffectProcess()
    {
        Vector3 position = _cubeSpawner.LastCube.position;
        position.y = _cubeSpawner.CurrentCube.transform.position.y -
                     _cubeSpawner.CurrentCube.transform.localScale.y * 0.5f;

        Vector3 scale = _cubeSpawner.CurrentCube.transform.localScale;
        scale = new Vector3(scale.x + addedSize, perfectEffect.localScale.y, scale.z + addedSize);
        
        OnPerfectEffect(position, scale);

        if (perfectCombo > 0 && perfectCombo < recoveryCombo)
            StartCoroutine(OnPerfectComboEffect(position, scale));
        else if (perfectCombo >= recoveryCombo)
        {
            _cubeSpawner.CurrentCube.RecoveryCube();
            OnPerfectRecoveryEffect();
        }
            
    }

    private void OnPerfectEffect(Vector3 position, Vector3 scale)
    {
        Transform effect  = Instantiate(perfectEffect);
        effect.position   = position;
        effect.localScale = scale;
    }
    
    private void SFXProcess()
    {
        int   maxCombo = 5;
        float volumeMin = 0.3f;
        float volumeAdditive = 0.15f;
        float pitchMin = 0.7f;
        float pitchAdditive = 0.15f;

        if (perfectCombo < maxCombo)
        {
            _audioSource.volume = volumeMin + perfectCombo * volumeAdditive;
            _audioSource.pitch  = pitchMin + perfectCombo * pitchAdditive;
        }
        
        _audioSource.Play();
    }
    
    private IEnumerator OnPerfectComboEffect(Vector3 position, Vector3 scale)
    {
        int   currentCombo = 0;
        float beginTime    = Time.time;
        float duration     = 0.15f;

        while (currentCombo < perfectCombo)
        {
            float t = (Time.time - beginTime) / duration;

            if (t >= 1)
            {
                Transform effect  = Instantiate(perfectComboEffect);
                effect.position   = position;
                effect.localScale = scale;

                beginTime = Time.time;

                currentCombo++;
            }

            yield return null;
        }
    }

    public void OnPerfectRecoveryEffect()
    {
        Transform effect = Instantiate(perfectRecoveryEffect);

        effect.position = _cubeSpawner.CurrentCube.transform.position;

        var shape = effect.GetComponent<ParticleSystem>().shape;
        float radius = _cubeSpawner.CurrentCube.transform.localScale.x > 
                       _cubeSpawner.CurrentCube.transform.localScale.z
                     ? _cubeSpawner.CurrentCube.transform.localScale.x
                     : _cubeSpawner.CurrentCube.transform.localScale.z;
        shape.radius = radius;
        shape.radiusThickness = radius * 0.5f;
    }
}
