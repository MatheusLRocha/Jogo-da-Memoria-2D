public interface IDataBase
{
    void Connect();
    void Disconnect();

    void Insert(PlayerModel data);
    void Update(PlayerModel data);

    int GetTotalPlayers();

    PlayerModel GetPlayerById(int id);

    PlayerModel GetPlayerByName(string name);
}