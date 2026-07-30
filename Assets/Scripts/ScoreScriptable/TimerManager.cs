using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;
public class TimerManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _timerText;

    public float _time;

    void Update()
    {
        _time = Time.timeSinceLevelLoad;
        DisplayTime(_time);
    }

    private void DisplayTime(float timeToDisplay)
    {
        _timerText.text = string.Format("{0:F2}", timeToDisplay);
        if (timeToDisplay >= 300.0f)
            Debug.Log("Demorou muito!");
    }

    public float GetTime()
    {
        return _time;
    }
}