using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisibleMode_Wav : MonoBehaviour
{

    [SerializeField]
    private GameObject visibleManager;
    [SerializeField]
    private Image      VisibleTime;
    
    private float      timeAmount = 1;

    public float TimeAmount
    {
        set
        {
            timeAmount = value;

            if (timeAmount <= 0)
            {
                timeAmount = 0;
                IsCoolDown = true;
                VisibleTime.color = Color.red;
            }
            else if (timeAmount >= 1)
            {
                timeAmount = 1;
                IsCoolDown = false;
                VisibleTime.color = Color.green;
            }
        }
        get => timeAmount;
    }

    public bool IsCoolDown { private set; get; } = false;

    public void UpdateVisibleMode(bool isCliked)
    {
        float decreaseAmount = 0.3f;
        float increaseAmountNormal = 0.5f;
        float increaseAmountTime = 0.3f;
        float activateAmount = 0.3f;

        if (IsCoolDown)
            TimeAmount += Time.deltaTime * increaseAmountTime;
        else
        {
            if (isCliked) TimeAmount -= Time.deltaTime * decreaseAmount;
            else          TimeAmount += Time.deltaTime * increaseAmountTime;
        }

        if (TimeAmount >= activateAmount || IsCoolDown)
            visibleManager.SetActive(true);

        VisibleTime.fillAmount = TimeAmount;
    }

    public void Deactivate()
    {
        visibleManager.SetActive(false);
    }

}
