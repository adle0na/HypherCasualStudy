using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailEffect_Ome : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float          duration = 0.5f;

    private SpriteRenderer _spriteRenderer;
    private Color          originColor;
    private Vector3        originScale;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originColor     = _spriteRenderer.color;
        originScale     = transform.localScale;
    }

    private void OnEnable()
    {
        _spriteRenderer.color = originColor;
        transform.localScale  = originScale;

        StartCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        float current = 0;
        float percent = 0;
        Vector3 start = transform.localScale;
        Vector3 end   = transform.localScale * 0.2f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / duration;

            Color color = _spriteRenderer.color;
            color.a = Mathf.Lerp(1, 0, curve.Evaluate(percent));
            _spriteRenderer.color = color;

            transform.localScale = Vector3.Lerp(start, end, curve.Evaluate(percent));

            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
