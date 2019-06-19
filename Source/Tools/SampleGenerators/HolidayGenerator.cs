using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Tools.SampleGenerators
{
    public class HolidayGenerator
    {
        private static readonly HolidayBuilder HolidayBuilder = new HolidayBuilder();

        public static Holiday CreateHoliday(int year = 2001, string description = " ", string name = "Workers Day")
        {
            return HolidayBuilder
                .Create()
                .WithDate(year, 5, 1)
                .WithName(name)
                .WithDescription(description)
                .Build();
        }
    }
}
