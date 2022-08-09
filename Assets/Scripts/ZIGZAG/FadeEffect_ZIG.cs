using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeEffect_ZIG : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 0.5f;

    [SerializeField]
    private AnimationCurve  _fadeCurve;
    private TextMeshProUGUI _fadeText;
    private float           endAlpha;

    private void Awake()
    {
        _fadeText = GetComponent<TextMeshProUGUI>();

        endAlpha = _fadeText.color.a;
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, endAlpha));
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color    = _fadeText.color;
            color.a        = Mathf.Lerp(start, end, _fadeCurve.Evaluate(percent));
            _fadeText.color = color;
            
            yield return null;
        }
    }
}
