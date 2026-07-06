using SQLite;

public class GameData
{
    [PrimaryKey, AutoIncrement]    
    public int id { get; set; }
    public string name { get; set; }
    public int points { get; set; } = 0;
}
