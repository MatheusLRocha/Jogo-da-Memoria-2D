using UnityEngine;
using TMPro;

public class PointsUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private PointsManager _pointsManager;

    void Update()
    {
        DisplayPoints();
    }

    private void DisplayPoints()
    {
        _scoreText.text = $"{_pointsManager.GetPoints():F2}";
    }
}