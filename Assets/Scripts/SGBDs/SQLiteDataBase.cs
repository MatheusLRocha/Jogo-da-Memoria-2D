using SQLite;
using System.Data;
using System.IO;
using UnityEngine;

public class SQLiteDataBase : IDataBase
{
    private SQLiteConnection _db;

    public SQLiteDataBase()
    {
        Connect();
    }

    public void Connect()
    {
        _db = new SQLiteConnection(Path.Combine(Application.persistentDataPath, "players.db"));
        _db.CreateTable<PlayerModel>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
    }

    public void Disconnect()
    {
        _db.Dispose();
    }

    public void Insert(PlayerModel data)
    {
        _db.Insert(data);
    }

    public void Update(PlayerModel data)
    {
        _db.Update(data);
    }

    public int GetTotalPlayers()
    {
        return _db.ExecuteScalar<int>("SELECT COUNT(*) FROM Players");
    }

    public PlayerModel GetPlayerById(int id)
    {
        return _db.Find<PlayerModel>(id);
    }

    public PlayerModel GetPlayerByName(string name)
    {
        return _db.Find<PlayerModel>(p => p.Name == name);
    }
}
