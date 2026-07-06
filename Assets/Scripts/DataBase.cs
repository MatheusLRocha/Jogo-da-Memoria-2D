using SQLite;
using System.IO;
using UnityEngine;

public class DataBase
{
    private SQLiteConnection _db;
    private string _dbPath;

    public DataBase()
    {
        _dbPath = Path.Combine(Application.persistentDataPath, "save_data.db");

        _db = new SQLiteConnection(_dbPath);
        
        _db.CreateTable<PlayerModel>();
    }
}
