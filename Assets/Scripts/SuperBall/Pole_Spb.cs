using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole_Spb : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }

}
