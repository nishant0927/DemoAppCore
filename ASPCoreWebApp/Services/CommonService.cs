
using ASPCoreWebApp.DB;
using Microsoft.EntityFrameworkCore;

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

        public async Task<T> Save(T data)
        {
           _dbSet.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }
    }
}
