using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsuDev.BusinessDays.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> Get(Func<TEntity, bool> expression);
        
        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> UpdateAsync(int id, TEntity entity);

        Task<bool> AnyAsync(TEntity entity);
    }
}
