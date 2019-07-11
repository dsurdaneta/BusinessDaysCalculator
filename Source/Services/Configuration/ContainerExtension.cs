using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using SimpleInjector;

namespace DsuDev.BusinessDays.Services.Configuration
{
    public static class ContainerExtension
    {
        public static Container AddRegisterReaders(this Container container)
        {
            container.Register<IJsonReader,JsonHolidayReader>();
            container.Register<IXmlReader,XmlHolidayReader>();
            container.Register<ICsvReader,CsvHolidayReader>();
            container.Register<ICustomTxtReader,CustomTxtHolidayReader>();
            container.Register<IFileReadingManager,FileReadingManager>();

            return container;
        }
    }
}
