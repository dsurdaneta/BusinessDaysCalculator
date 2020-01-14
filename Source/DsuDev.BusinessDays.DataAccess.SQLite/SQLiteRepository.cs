using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    public class SQLiteRepository : IRepository<Holiday>
    {
        public bool Delete(Holiday entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Holiday> Get()
        {
            throw new NotImplementedException();
        }

        public Holiday Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Holiday> Get(Expression<Func<Holiday, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Holiday entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, Holiday entity)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
