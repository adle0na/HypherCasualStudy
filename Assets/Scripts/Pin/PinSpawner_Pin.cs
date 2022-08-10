using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner_Pin : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private StageController_Pin _stageController;
    [SerializeField]
    private GameObject          pinPrefab;
    [SerializeField]
    private GameObject          textPinIndexPrefab;
    [SerializeField]
    private Transform           textParent;

    [Header("Stuck Pin")]
    [SerializeField]
    private Transform           targetTransform;
    [SerializeField]
    private Vector3             targetPosition = Vector3.up * 2;
    [SerializeField]
    private float               targetRadius   = 0.8f;
    [SerializeField]
    private float               pinLength      = 1.5f;

    [Header("Throwable Pin")]
    [SerializeField]
    private float bottomAngle = 270;

    private List<Pin_Pin> throwablePins;

    private AudioSource   _audioSource;

    public void Setup()
    {
        _audioSource = GetComponent<AudioSource>();
        throwablePins = new List<Pin_Pin>();
    }

    private void Update()
    {
        if (_stageController.IsGameStart == false || _stageController.IsGameOver == true) return;
        
        if (Input.GetMouseButtonDown(0) && throwablePins.Count > 0)
        {
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            throwablePins.RemoveAt(0);

            for (int i = 0; i < throwablePins.Count; ++i)
            {
                throwablePins[i].MoveOneStep(_stageController.TPinDistance);
            }
            
            _stageController.DecreaseThrowablePin();

            _audioSource.Play();
        }
    }

    public void SpawnThrowablePin(Vector3 position, int index)
    {
        GameObject clone = Instantiate(pinPrefab, position, Quaternion.identity);

        Pin_Pin pin = clone.GetComponent<Pin_Pin>();
        pin.Setup(_stageController);
        
        throwablePins.Add(pin);

        SpawnTextUI(clone.transform, index);
    }

    public void SpawnStuckPin(float angle, int index)
    {
        GameObject clone = Instantiate(pinPrefab);

        Pin_Pin pin = clone.GetComponent<Pin_Pin>();
        
        pin.Setup(_stageController);

        SetInPinStuckToTarget(clone.transform, angle);
        
        SpawnTextUI(clone.transform, index);
    }

    private void SetInPinStuckToTarget(Transform pin, float angle)
    {
        pin.position = Utils_Pin.GetPositionFromAngle(targetRadius + pinLength, angle) + targetPosition;
        pin.rotation = Quaternion.Euler(0, 0, angle);
        pin.SetParent(targetTransform);
        pin.GetComponent<Pin_Pin>().SetInPinStuckToTarget();
    }

    private void SpawnTextUI(Transform target, int index)
    {
        GameObject textClone = Instantiate(textPinIndexPrefab);
        textClone.transform.SetParent(textParent);
        textClone.transform.localScale = Vector3.one;
        textClone.GetComponent<WorldToScreenPosition_Pin>().Setup(target);
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }
}
