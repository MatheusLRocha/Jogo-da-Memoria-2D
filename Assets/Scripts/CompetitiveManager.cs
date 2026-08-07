using UnityEngine;
using System;

public class CompetitiveManager : MonoBehaviour
{
    [SerializeField] private PointsManager _pointsManager;

    [SerializeField] private TimerManager _timerManager;

    private string _username;

    private float _oldPoints;

    public PlayerModel player;

    public SQLiteDataBase _db;

    void Awake()
    {
        _username = PlayerPrefs.GetString("Username");

        _db = new SQLiteDataBase();
    }

    void Start()
    {
        player = LoadPlayer();

        _oldPoints = (float)player.Points;
    }

    void Update()
    {
        if (!HasNewScore()) return;

        UpdatePlayerProgress();
    }

    private PlayerModel LoadPlayer()
    {
        PlayerModel player = _db.GetPlayerByName(_username);

        if (player != null) return player;

        player = CreatePlayer();
        _db.Insert(player);

        return player;
    }

    public PlayerModel CreatePlayer()
    {
        PlayerModel player = new PlayerModel
        {
            Name = _username,
            Points = (decimal)_pointsManager.GetPoints(),
            Time = (decimal)_timerManager.GetTime(),
        };

        return player;
    }

    private bool HasNewScore()
    {
        return _pointsManager.GetPoints() > _oldPoints;
    }

    private void UpdatePlayerProgress()
    {
        player.Points =  Math.Round((decimal)_pointsManager.GetPoints(), 2);
        player.Time = Math.Round((decimal)_timerManager.GetTime(), 2);

        _db.Update(player);

        _oldPoints = _pointsManager.GetPoints();
    }

    private void OnDestroy()
    {
        _db?.Disconnect();
    }
}