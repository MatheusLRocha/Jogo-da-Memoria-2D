using SQLite;

[Table("Players")]
public class PlayerModel
{
    [Column("name"), Unique, NotNull]
    public string Name { get; set; }
    
    [Column("points")]
    public int Points { get; set; } = 0;
}
