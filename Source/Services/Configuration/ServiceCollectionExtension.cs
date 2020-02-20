using DsuDev.BusinessDays.Services.FileHandling;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.DataAccess.SQLite;

using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Services.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {
        /// <summary>Adds the Business Days services.</summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessDaysServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IFileLoader), typeof(FileLoader));
            services.AddSingleton(typeof(IDataProvider), typeof(DataProvider));
            services.AddSingleton(typeof(ICalculator), typeof(Calculator));
            return services;
        }

        /// <summary>Adds the business days data access.</summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessDaysDataAccess(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IContext), typeof(HolidaysSQLiteContext));
            services.AddSingleton(typeof(IRepository<DbModels.Holiday>), typeof(SQLiteRepository));
            return services;
        }
        
        public static IServiceCollection AddBusinessDaysFileReaders(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IJsonReader), typeof(JsonHolidayReader));
            services.AddSingleton(typeof(IXmlReader), typeof(XmlHolidayReader));
            services.AddSingleton(typeof(ICsvHolidayReader), typeof(CsvHolidayReader));
            services.AddSingleton(typeof(ICustomTxtReader), typeof(CustomTxtHolidayReader));
            services.AddSingleton(typeof(IFileReadingManager), typeof(FileReadingManager));
            return services;
        }


    }
}
