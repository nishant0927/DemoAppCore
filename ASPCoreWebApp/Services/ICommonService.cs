namespace ASPCoreWebApp.Services
{
    public interface ICommonService<T>
    {
        Task<List<T>> getAllData();
        Task<T> Save(T data);
    }
}
