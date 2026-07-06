using SQLite;

[Table("Players")]
public class PlayerModel
{
    [PrimaryKey, AutoIncrement]    
    public int id { get; set; }

    [Unique]
    public string name { get; set; }
    
    public int points { get; set; } = 0;
}
