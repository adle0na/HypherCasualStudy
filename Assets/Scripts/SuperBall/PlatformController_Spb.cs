using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController_Spb : MonoBehaviour
{
    [SerializeField]
    private float                    removeDuration = 1;

    public bool IsCollision { private set; get; } = false;

    public void BreakAllParts()
    {
        if (IsCollision == false)
            IsCollision = true;

        if (transform.parent != null)
            transform.parent = null;

        PlatformPartController_Spb[] parts = transform.GetComponentsInChildren<PlatformPartController_Spb>();
        
        foreach (PlatformPartController_Spb part in parts)
            part.BreakingPart();

        StartCoroutine(nameof(RemoveParts));
    }

    private IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(removeDuration);
        
        gameObject.SetActive(false);
    }
}
