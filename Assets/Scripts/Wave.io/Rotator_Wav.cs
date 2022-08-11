using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_Wav : MonoBehaviour
{
    [SerializeField]
    private float   rotateSpeed     = 50;
    private Vector3 rotateDirection = Vector3.forward;

    private void Update()
    {
        transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }

}
