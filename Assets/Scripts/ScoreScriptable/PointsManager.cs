using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private float _currentPoints = 0.0f;
    private const float MAX_POINTS = 100.0f;

    public void SetPoints(float value)
    {
        _currentPoints += value;
        UpdateScoreText();
    }

    public float GetPoints()
    {
        return _currentPoints;
    }

    public void UpdateScoreText()
    {
        _scoreText.text = $"{_currentPoints:F2}";
    }
}