public interface IDataBase
{
    void Connect();
    void Disconnect();

    void Insert<T>(T data);
    void Update<T>(T data);
    void Delete<T>(T data);
}