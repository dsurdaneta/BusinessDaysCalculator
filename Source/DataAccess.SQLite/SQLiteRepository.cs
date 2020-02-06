using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    /// <summary>
    /// The SQLite repository
    /// </summary>
    /// <seealso cref="DsuDev.BusinessDays.DataAccess.IRepository{DsuDev.BusinessDays.DataAccess.Models.Holiday}" />
    public class SQLiteRepository : IRepository<DbModels.Holiday>
    {
        private readonly IHolidayContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SQLiteRepository(IHolidayContext context)
        {
            this.dbContext = context;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(DbModels.Holiday entity)
        {
            this.dbContext.Holidays.Remove(entity);
            int result = await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc />
        public async Task<ICollection<DbModels.Holiday>> GetAllAsync()
        {
            return await this.dbContext.Holidays.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DbModels.Holiday> GetByIdAsync(int id)
        {
            return await this.dbContext.Holidays.FirstOrDefaultAsync(x => x.Id.Equals(id))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public ICollection<DbModels.Holiday> Get(Func<DbModels.Holiday, bool> expression)
        {
            return this.dbContext.Holidays.AsParallel().Where(expression).ToList();
        }

        /// <inheritdoc />
        public async Task<DbModels.Holiday> AddAsync(DbModels.Holiday entity)
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
        public async Task<bool> UpdateAsync(int id, DbModels.Holiday entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            this.dbContext.Entry(entity).State = EntityState.Modified;
            int result = await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync(DbModels.Holiday entity)
        {
            return await this.dbContext.Holidays.AnyAsync(x => x.Id.Equals(entity.Id) || x.HolidayDate.Equals(entity.HolidayDate))
                .ConfigureAwait(false);
        }
    }
}
