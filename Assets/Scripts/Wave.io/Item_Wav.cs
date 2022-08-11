using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Wav : MonoBehaviour
{
    [SerializeField] private GameObject itemEffect;

    public void Exit()
    {
        Instantiate(itemEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

}
