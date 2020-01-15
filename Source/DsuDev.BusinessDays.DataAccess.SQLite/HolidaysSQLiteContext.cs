using DsuDev.BusinessDays.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    public class HolidaysSQLiteContext : DbContext, IContext
    {
        private const string DbName = "bussinessdays.sqlite";
        private static readonly string DefaultConnectionString = $"Data Source={DbName};Version=3;";
        private static bool _isDbRecentlyCreated = false;
        
        public DbSet<Holiday> Holidays { get; set; }

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
    }
}
