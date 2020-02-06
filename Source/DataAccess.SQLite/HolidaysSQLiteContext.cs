using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    /// <summary>
    /// The Context for the SQLite database
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="DsuDev.BusinessDays.DataAccess.IHolidayContext" />
    public class HolidaysSQLiteContext : DbContext, IHolidayContext
    {
        private const string DbName = "bussinessdays.sqlite";
        private static readonly string DefaultConnectionString = $"Data Source={DbName};Version=3;";
        private static bool _isDbRecentlyCreated = false;
        
        public DbSet<DbModels.Holiday> Holidays { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidaysSQLiteContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public HolidaysSQLiteContext(DbContextOptions options) : base(options)
        {

        }
        
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DefaultConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _isDbRecentlyCreated = true;
        }

        EntityEntry<DbModels.Holiday> IHolidayContext.Entry(DbModels.Holiday entity)
        {
            return base.Entry(entity);
        }

        async Task<int> IHolidayContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
