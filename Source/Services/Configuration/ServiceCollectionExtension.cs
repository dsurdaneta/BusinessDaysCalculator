using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.DataAccess.SQLite;
using DsuDev.BusinessDays.Services.FileHandling;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DsuDev.BusinessDays.Services.Profiles;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Services.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {
        /// <summary>Adds all business days services.</summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddAllBusinessDays(this IServiceCollection services)
        {
            return services.AddThirdParty()
                            .AddBusinessDaysDataAccess()
                            .AddBusinessDaysFileReaders()
                            .AddBusinessDaysServices();
        }

        public static IServiceCollection AddThirdParty(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new HolidayContractProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        
        /// <summary>Adds the Business Days services.</summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessDaysServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFileLoader), typeof(FileLoader));
            services.AddSingleton(typeof(IDataProvider), typeof(DataProvider));
            services.AddScoped(typeof(ICalculator), typeof(Calculator));

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

        /// <summary>Adds the business days file readers.</summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessDaysFileReaders(this IServiceCollection services)
        {
            services.AddTransient(typeof(IJsonReader), typeof(JsonHolidayReader));
            services.AddTransient(typeof(IXmlReader), typeof(XmlHolidayReader));
            services.AddTransient(typeof(ICsvHolidayReader), typeof(CsvHolidayReader));
            services.AddTransient(typeof(ICustomTxtReader), typeof(CustomTxtHolidayReader));
            services.AddTransient(typeof(IFileReadingManager), typeof(FileReadingManager));

            return services;
        }
    }
}
