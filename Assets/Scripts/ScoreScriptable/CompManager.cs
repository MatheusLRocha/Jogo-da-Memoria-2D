using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class CompManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    public float time;

    void Start()
    {

    }


    void Update()
    {
        time = Time.timeSinceLevelLoad;
        DisplayTime(time);
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = timeToDisplay;
        _timerText.text = string.Format("{0:F2}",seconds);
    }
}
