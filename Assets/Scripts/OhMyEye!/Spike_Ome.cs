using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Ome : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.5f;

    public void OnMove(float x)
    {
        Vector2 start = transform.position;
        Vector2 end   = new Vector2(x, transform.position.y);

        StartCoroutine(MoveProcess(start, end));
    }

    private IEnumerator MoveProcess(Vector2 start, Vector2 end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector2.Lerp(start, end, percent);

            yield return null;
        }
    }

}
