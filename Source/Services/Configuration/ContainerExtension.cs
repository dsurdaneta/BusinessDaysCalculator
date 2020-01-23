using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.DataAccess.SQLite;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Services.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ContainerExtension
    {
        public static Container RegisterAll(this Container container)
        {
            RegisterDataAccess(container);
            RegisterFileReaders(container);
            RegisterServices(container);
            return container;
        }

        /// <summary>
        /// Registers all the needed FileReader classes into the given container
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Container RegisterFileReaders(this Container container)
        {
            container.Register<IJsonReader,JsonHolidayReader>();
            container.Register<IXmlReader,XmlHolidayReader>();
            container.Register<ICsvHolidayReader,CsvHolidayReader>();
            container.Register<ICustomTxtReader,CustomTxtHolidayReader>();
            container.Register<IFileReadingManager,FileReadingManager>();

            return container;
        }

        /// <summary>
        /// Registers the data access classes.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static Container RegisterDataAccess(this Container container)
        {
            container.Register<IContext,HolidaysSQLiteContext>();
            container.Register<IRepository<DbModels.Holiday>, SQLiteRepository>();
            return container;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static Container RegisterServices(this Container container)
        {
            container.Register<IFileLoader, FileLoader>();
            container.Register<IDataProvider, DataProvider>();
            //container.Register<ICalculator, Calculator>();
            return container;
        }

        /// <summary>
        /// Determines whether this configuration instance is loaded.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if the specified configuration is loaded; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }
    }
}
