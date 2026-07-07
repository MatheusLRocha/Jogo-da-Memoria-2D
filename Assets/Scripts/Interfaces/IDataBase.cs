public interface IDataBase
{
    void Connect();
    void Disconnect();

    void Create<T>(T data);
    void Update<T>(T data);
    void Delete<T>(T data);
}