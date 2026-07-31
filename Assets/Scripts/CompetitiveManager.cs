using Mono.Cecil.Cil;
using UnityEngine;
using System;

public class CompetitiveManager : MonoBehaviour
{
    [SerializeField] private PointsManager _pointsManager;

    [SerializeField] private TimerManager _timerManager;

    private string _username;

    private float _oldPoints;

    private PlayerModel player;

    private SQLiteDataBase _db;

    void Awake()
    {
        _username = PlayerPrefs.GetString("Username");

        _db = new SQLiteDataBase();
    }

    void Start()
    {
        player = _db.GetPlayerByName(_username);

        if (player == null)
        {
            player = new PlayerModel
            {
                Name = _username,
                Points = (decimal)_pointsManager.GetPoints(),
                Time = (decimal)_timerManager.GetTime(),
            };

            _db.Insert(player);
        }
    }

    void Update()
    {
        if (_pointsManager.GetPoints() > _oldPoints)
        {
            player.Points =  Math.Round((decimal)_pointsManager.GetPoints(), 2);
            player.Time = Math.Round((decimal)_timerManager.GetTime(), 2);

            _db.Update(player);
        }

        _oldPoints = _pointsManager.GetPoints();
    }

    private void OnDestroy()
    {
        _db?.Disconnect();
    }
}