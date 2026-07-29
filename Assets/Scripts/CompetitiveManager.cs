using UnityEngine;

public class CompetitiveManager : MonoBehaviour
{
    [SerializeField] private PointsManager _pointsManager;

    [SerializeField] private TimerManager _timerManager;

    private string _username;

    private SQLiteDataBase _db;

    void Awake()
    {
        _username = PlayerPrefs.GetString("Username");

        _db = new SQLiteDataBase();

        _db.Connect();
    }

    void Start()
    {
        var player = new PlayerModel
        {
            Name = _username,
            Points = _pointsManager.GetPoints(),
            Time = _timerManager.GetTime(),
        };

        _db.Insert(player);

        Debug.Log(player.Name);
        Debug.Log(player.Points);
        Debug.Log(player.Time);
    }
}
