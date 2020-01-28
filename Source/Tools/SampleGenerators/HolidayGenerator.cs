using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.Common.Tools.FluentBuilders;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Common.Tools.SampleGenerators
{
    /// <summary>
    /// Class to help the sample creation of a Holiday object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HolidayGenerator
    {
        private static readonly HolidayBuilder HolidayBuilder = new HolidayBuilder();

        public static Holiday CreateHoliday(int year = 2001, int month = 5, int day = 1, string description = " ", string name = "Workers Day", int id = 0)
        {
            return HolidayBuilder
                .Create()
                .WithId(id)
                .WithDate(year, month, day)
                .WithName(name)
                .WithDescription(description)
                .Build();
        }

        public static List<Holiday> CreateHolidays(int amount, int baseYear)
        {
            var holidays = new List<Holiday>();
            
            for (int i = 0; i < amount; i++)
            {
                var randomHoliday = CreateHoliday(
                    baseYear,
                    RandomValuesGenerator.RandomInt(1, 12),
                    RandomValuesGenerator.RandomInt(1, 28),
                    RandomValuesGenerator.RandomString(30),
                    RandomValuesGenerator.RandomString(10),
                    RandomValuesGenerator.RandomInt(1, amount*3));

                holidays.Add(randomHoliday);
            }

            return holidays;
        }
    }
}
