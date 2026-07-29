using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _time;

    void Update()
    {
        _time = Time.timeSinceLevelLoad;
        DisplayTime(_time);
    }

    private void DisplayTime(float timeToDisplay)
    {
        _timerText.text = string.Format("{0:F2}", timeToDisplay);
    }

    public float GetTime()
    {
        return _time;
    }
}