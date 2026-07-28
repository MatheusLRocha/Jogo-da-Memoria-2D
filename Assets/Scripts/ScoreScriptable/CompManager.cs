using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class CompManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _time;

    [SerializeField] public PointsManager pointsManager;
    

    void Update()
    {
        _time = Time.timeSinceLevelLoad;
        DisplayTime(_time);
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = timeToDisplay;
        _timerText.text = string.Format("{0:F2}",seconds);
    }

    public float GetTime()
    {
        return _time;
    }
}
