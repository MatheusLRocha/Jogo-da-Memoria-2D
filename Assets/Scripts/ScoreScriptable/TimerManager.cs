using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;
public class TimerManager : MonoBehaviour
{
    private float _time;

    private float _maxTime = 150.0f;

    void Update()
    {
        _time = Time.timeSinceLevelLoad;

        TimeHasExpired(_time);
    }

    private void TimeHasExpired(float time)
    {
        if (time >= _maxTime)
            Debug.Log("Demorou muito!");
    }

    public float GetTime()
    {
        return _time;
    }
}