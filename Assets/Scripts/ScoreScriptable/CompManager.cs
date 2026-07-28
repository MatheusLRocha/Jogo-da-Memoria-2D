using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;
public class CompManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    public float time;
    void Update()
    {
        DisplayTime(time);
    }

    void DisplayTime(float timeToDisplay)
    {
        time = Time.timeSinceLevelLoad;
        float seconds = timeToDisplay;
        _timerText.text = string.Format("{0:F2}",seconds);
        if (time >= 300.0f)
            Debug.Log("Demorou muito!");
    }
}
