using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;
public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _time;

    void Update()
    {
        DisplayTime(time);
    }

    private void DisplayTime(float timeToDisplay)
    {
        _timerText.text = string.Format("{0:F2}", timeToDisplay);
    }

    public float GetTime()
    {
        _time = Time.timeSinceLevelLoad;
        float seconds = timeToDisplay;
        _timerText.text = string.Format("{0:F2}",seconds);
        if (_time >= 300.0f)
            Debug.Log("Demorou muito!");
        return _time;
    }
}