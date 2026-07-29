using SQLite;

[Table("Players")]
public class PlayerModel
{
    [Column("name"), Unique, NotNull]
    public string Name { get; set; }
    
    [Column("points"), NotNull]
    public float Points { get; set; } = 0;

    [Column("time"), NotNull]
    public float Time { get; set; } = 0;
}
