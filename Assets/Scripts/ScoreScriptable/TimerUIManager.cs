using UnityEngine;
using TMPro;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TimerManager _timerManager;

    void Update()
    {
        DisplayTime(_timerManager.GetTime());
    }

    private void DisplayTime(float timeToDisplay)
    {
        _timerText.text = string.Format("{0:F2}", timeToDisplay);
    }
}
