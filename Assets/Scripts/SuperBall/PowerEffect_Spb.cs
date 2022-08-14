using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEffect_Spb : MonoBehaviour
{
    public void ChangeColor(Color color)
    {
        var Module = GetComponent<ParticleSystem>().main;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] 
            {
                new GradientColorKey(Color.white, 0),
                new GradientColorKey(color, 0.5f),
                new GradientColorKey(Color.black, 1)
            }, 
            new GradientAlphaKey[] 
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 0.5f),
                new GradientAlphaKey(1, 1)
            }
        );

        Module.startColor = gradient;
    }
}
