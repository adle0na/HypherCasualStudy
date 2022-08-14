using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformPartController_Spb : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    private     MeshRenderer _meshRenderer;
    private new Rigidbody    _rigidbody;
    private new Collider     _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody    = GetComponent<Rigidbody>();
        _collider     = GetComponent<Collider>();
    }

    public void BreakingPart()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled      = false;

        Vector3 forcePoint    = transform.parent.position;
        float parentXPosition = transform.parent.position.x;
        float xPosition       = _meshRenderer.bounds.center.x;

        Vector3 direction     = (parentXPosition - xPosition < 0) ? Vector3.right : Vector3.left;
        direction             = (Vector3.up * moveSpeed + direction).normalized;

        float force           = Random.Range(20, 40);
        float torque          = Random.Range(110, 180);

        _rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left * torque);
        _rigidbody.velocity = Vector3.down;
    }
}
