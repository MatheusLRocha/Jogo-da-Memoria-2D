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
    }

    public void Disconnect()
    {
        _db.Dispose();
    }

    public void Create<T>(T data)
    {
        _db.CreateTable<PlayerModel>();
    }

    public void Update<T>(T data)
    {
        
    }

    public void Delete<T>(T data)
    {
        
    }
}
