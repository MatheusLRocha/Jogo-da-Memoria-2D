using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private float _currentPoints = 0.0f;

    public void AddPoints(float time)
    {
        switch (time)
        {
            case <= 15.0f:
                SetPoints(700f - time);
                break;
            case <= 30.0f:
                SetPoints(600f - time);
                break;
            case <= 45.0f:
                SetPoints(550f - time);
                break;
            case <= 60.0f:
                SetPoints(450f - time);
                break;
            case <= 75.0f:
                SetPoints(300f - time);
                break;
            case <= 90.0f:
                SetPoints(275f - time);
                break;
            case <= 105.0f:
                SetPoints(200f - time);
                break;
            default:
                SetPoints(175f - time);
                break;
        }
    }
    
    private void SetPoints(float value)
    {
        _currentPoints += value;
    }

    public float GetPoints()
    {
        return _currentPoints;
    }
}