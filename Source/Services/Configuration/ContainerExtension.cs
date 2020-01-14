using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using SimpleInjector;
using Microsoft.Extensions.Configuration;

namespace DsuDev.BusinessDays.Services.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ContainerExtension
    {
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

        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }
    }
}
