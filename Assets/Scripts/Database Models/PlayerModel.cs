using SQLite;

[Table("Players")]
public class PlayerModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("name"), Unique, NotNull]
    public string Name { get; set; }
    
    [Column("points"), NotNull]
    public decimal Points { get; set; } = 0;

    [Column("time"), NotNull]
    public decimal Time { get; set; } = 0;
}
