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
        _time = Time.timeSinceLevelLoad;
        DisplayTime(_time);
    }

    private void DisplayTime(float timeToDisplay)
    {
        _timerText.text = string.Format("{0:F2}", timeToDisplay);
        if (_time >= 300.0f)
        Debug.Log("Demorou muito!");
    }

    public float GetTime()
    {
        //_timerText.text = string.Format("{0:F2}", _time);
        return _time;
    }
}