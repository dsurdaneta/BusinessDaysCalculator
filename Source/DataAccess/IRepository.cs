using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DsuDev.BusinessDays.DataAccess
{
    public interface IRepository<T> where T : class
    {
        bool Insert(T entity);

        IEnumerable<T> Get();

        T Get(string id);

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
        
        bool Delete(T entity);

        bool Update(string id, T entity);

        bool Save();
    }
}
