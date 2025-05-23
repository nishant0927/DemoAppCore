
using ASPCoreWebApp.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ASPCoreWebApp.Services
{
    public class CommonService<T> : ICommonService<T> where T : class
    {
        private readonly DBContex _dbContext;
        private DbSet<T> _dbSet;
        public CommonService(DBContex dbContext)
        {
            _dbContext = dbContext;
            _dbSet=_dbContext.Set<T>();
        }
        public async Task<List<T>> getAllData()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = includes.Aggregate(_dbSet.AsQueryable(),
                                             (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<T> GetFilterDataAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync() ?? null;
        }
        public async Task<List<T>> GetAllFilterDataAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
        }
        public async Task<T> Save(T data)
        {
           _dbSet.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }
    }
}
