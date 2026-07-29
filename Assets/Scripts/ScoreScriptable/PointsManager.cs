using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _currentPoints = 0.0f;

    public void AddPoints(float time)
    {
        SetPoints(10000f / time);
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