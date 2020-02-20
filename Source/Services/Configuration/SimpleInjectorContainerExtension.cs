using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.DataAccess.SQLite;
using DsuDev.BusinessDays.Services.FileHandling;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using SimpleInjector;
using System.Diagnostics.CodeAnalysis;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Services.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class SimpleInjectorContainerExtension
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
            container.Register<ICalculator, Calculator>();
            return container;
        }
    }
}
