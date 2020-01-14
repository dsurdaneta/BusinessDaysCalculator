using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using DsuDev.BusinessDays.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace DsuDev.BusinessDays.DataAccess.SQLite
{
    public class Context : DbContext, IContext
    {
        private const string DBName = "database.sqlite";
        private const string SQLScript = @"bussinessdays.sqlite";
        private static bool _isDbRecentlyCreated = false;
        
        public DbSet<Holiday> Holidays { get; set; }
        
        public static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection($"Data Source={DBName};Version=3;");

            db.Open();

            return db;
        }
        
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DBName};Version=3;");
        }
    }
}
