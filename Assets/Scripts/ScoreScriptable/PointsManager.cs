using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _currentPoints = 0;
    private const int MAX_POINTS = 5;

    public void SetPoints(int value)
    {
        _currentPoints += value;
        UpdateScoreText();
    }

    public int GetPoints()
    {
        return _currentPoints;
    }

    public void UpdateScoreText()
    {
        _scoreText.text = $"{_currentPoints}/{MAX_POINTS}";
    }
}