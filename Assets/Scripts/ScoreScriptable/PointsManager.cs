using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _currentPoints = 0.0f;

    public void AddPoints(float time)
    {
        if (time <= 15.0f)
            SetPoints(700f - time);
        else if (15f < time && time <= 30f)
            SetPoints(600f - time);
        else if (30f < time && time <= 45f)
            SetPoints(550f - time);
        else if (45f < time && time <= 60f)
            SetPoints(450f - time);
        else if (60f < time && time <= 75f)
            SetPoints(350f - time);
        else if (75f < time && time <= 90f)
            SetPoints(300f - time);
        else if (90f < time && time <= 105f)
            SetPoints(275f - time);
        else if (105f < time && time <= 120f)
            SetPoints(200f - time);
        else if (120f < time && time <= 150f)
            SetPoints(175f - time);
    }
    
    private void SetPoints(float value)
    {
        _currentPoints += value;
        _scoreText.text = $"{_currentPoints:F2}";
    }

    public float GetPoints()
    {
        return _currentPoints;
    }
}