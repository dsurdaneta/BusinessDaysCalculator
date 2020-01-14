using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DbEntites = DsuDev.BusinessDays.DataAccess.Entites;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    public class SQLiteRepository : IRepository<DbEntites.Holiday>
    {
        private Context dbContext;

        public bool Delete(DbEntites.Holiday entity)
        {
            this.dbContext.Holidays.Remove(entity);
            this.dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<DbEntites.Holiday> Get()
        {
            return this.dbContext.Holidays;
        }

        public DbEntites.Holiday Get(string id)
        {
            return this.dbContext.Holidays.FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<DbEntites.Holiday> Get(Expression<Func<DbEntites.Holiday, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Insert(DbEntites.Holiday entity)
        {
            if (this.dbContext.Holidays.Any(x => x.Id.Equals(entity.Id) || x.HolidayDate.Equals(entity.HolidayDate)))
            {
                return false;
            }
            entity.CreatedDateTime = DateTime.UtcNow;
            this.dbContext.Holidays.Add(entity);
            this.dbContext.SaveChanges();
            return true;
        }

        public bool Update(string id, DbEntites.Holiday entity)
        {
            throw new NotImplementedException();
        }}
}
