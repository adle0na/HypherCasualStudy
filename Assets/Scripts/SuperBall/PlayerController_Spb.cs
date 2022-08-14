using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController_Spb : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameController_Spb _gameController;
    
    [Header("Parameters")]
    [SerializeField]
    private float              bounceForce = 5;
    [SerializeField]
    private float              dropForce = -10;
    
    [Header("SFX")]
    [SerializeField]
    private AudioClip          bounceClip;
    [SerializeField]
    private AudioClip          normalBreakClip;
    
    [Header("VFX")]
    [SerializeField]
    private Material           playerMaterial;
    [SerializeField]
    private Transform          splashImg;
    [SerializeField]
    private ParticleSystem[]   splashParticles;
    
    private new Rigidbody    _rigidbody;
    private     AudioSource  _audioSource;

    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);
    private bool    isClicked    = false;
    private void Awake()
    {
        _rigidbody   = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_gameController.IsGamePlay) return;
        
        UpdateMouseButton();
        UpdateDropToSmash();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isClicked)
        {
            if (_rigidbody.velocity.y > 0) return;
            
            OnJumpProcess(collision);
        }
        else
        {
            if (collision.gameObject.CompareTag("BreakPart"))
            {
                var platform = collision.transform.parent.GetComponent<PlatformController_Spb>();
                _rigidbody.velocity = new Vector3(0, dropForce, 0);

                if (platform.IsCollision == false)
                {
                    platform.BreakAllParts();
                    
                    PlaySound(normalBreakClip);
                    
                    _gameController.OnCollisionWithPlatform();
                }
            }
            else if (collision.gameObject.CompareTag("NonBreakPart"))
            {
                _rigidbody.isKinematic = true;
                
                Debug.Log("GameOver");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_rigidbody.velocity.y > 0) return;

        OnJumpProcess(collision);
    }

    private void OnSplashImg(Transform target)
    {
        Transform image = Instantiate(splashImg, target);

        image.position         = transform.position - splashWeight;
        image.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        float randomScale      = Random.Range(0.3f, 0.5f);
        image.localScale       = new Vector3(randomScale, randomScale, 1);
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    
    private void OnSplashParticle()
    {
        for (int i = 0; i < splashParticles.Length; ++i)
        {
            if ( splashParticles[i].gameObject.activeSelf) continue;
            
            splashParticles[i].gameObject.SetActive(true);

            splashParticles[i].transform.position = transform.position - splashWeight;

            var mainModule = splashParticles[i].main;
            mainModule.startColor = playerMaterial.color;
            break;
        }
    }
    
    private void OnJumpProcess(Collision collision)
    {
        _rigidbody.velocity = new Vector3(0, bounceForce, 0);

        PlaySound(bounceClip);
        
        OnSplashImg(collision.transform);
        
        OnSplashParticle();
    }
    
    private void UpdateMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
            isClicked = true;
        else if (Input.GetMouseButtonUp(0))
            isClicked = false;
    }
    
    private void UpdateDropToSmash()
    {
        if (Input.GetMouseButtonDown(0) && isClicked)
            _rigidbody.velocity = new Vector3(0, dropForce, 0);
    }
}
