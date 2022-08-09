using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private GameObject pinPrefab;

    public void SpawnThrowablePin(Vector3 position)
    {
        Instantiate(pinPrefab, position, Quaternion.identity);
    }

}
