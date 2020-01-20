using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DsuDev.BusinessDays.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using DbEntites = DsuDev.BusinessDays.DataAccess.Entites;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    /// <summary>
    /// The SQLite repository
    /// </summary>
    /// <seealso cref="DsuDev.BusinessDays.DataAccess.IRepository{DsuDev.BusinessDays.DataAccess.Entites.Holiday}" />
    public class SQLiteRepository : IRepository<DbEntites.Holiday>
    {
        private readonly HolidaysSQLiteContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SQLiteRepository(HolidaysSQLiteContext context)
        {
            this.dbContext = context;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(DbEntites.Holiday entity)
        {
            this.dbContext.Holidays.Remove(entity);
            int result = await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DbEntites.Holiday>> GetAllAsync()
        {
            return await this.dbContext.Holidays.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DbEntites.Holiday> GetByIdAsync(int id)
        {
            return await this.dbContext.Holidays.FirstOrDefaultAsync(x => x.Id.Equals(id))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IEnumerable<DbEntites.Holiday> Get(Func<DbEntites.Holiday, bool> expression)
        {
            return this.dbContext.Holidays.AsParallel().Where(expression);
        }

        /// <inheritdoc />
        public async Task<DbEntites.Holiday> AddAsync(DbEntites.Holiday entity)
        {
            if (await this.AnyAsync(entity).ConfigureAwait(false))
            {
                return null;
            }
            entity.CreatedDate = DateTime.UtcNow;
            this.dbContext.Holidays.Add(entity);
            int result = await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0 ? entity : null;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, DbEntites.Holiday entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            this.dbContext.Entry(entity).State = EntityState.Modified;
            int result = await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync(Holiday entity)
        {
            return await this.dbContext.Holidays.AnyAsync(x => x.Id.Equals(entity.Id) || x.HolidayDate.Equals(entity.HolidayDate))
                .ConfigureAwait(false);
        }
    }
}
