using System.Linq.Expressions;

namespace ASPCoreWebApp.Services
{
    public interface ICommonService<T>
    {
        Task<List<T>> getAllData();
        Task<List<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetFilterDataAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllFilterDataAsync(Expression<Func<T, bool>> filter);
        Task<T> Save(T data);
    }
}
