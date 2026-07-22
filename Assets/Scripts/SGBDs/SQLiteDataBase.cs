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

    public void Insert<T>(T data)
    {
       _db.Insert(data);
    }

    public void Update<T>(T data)
    {
        
    }

    public void Delete<T>(T data)
    {
        
    }
}
