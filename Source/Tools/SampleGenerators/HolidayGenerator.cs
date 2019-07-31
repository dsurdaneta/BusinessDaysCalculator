using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Tools.SampleGenerators
{
    /// <summary>
    /// Class to help the creation of a Holiday object
    /// </summary>
    public class HolidayGenerator
    {
        private static readonly HolidayBuilder HolidayBuilder = new HolidayBuilder();

        public static Holiday CreateHoliday(int year = 2001, int month = 5, int day = 1, string description = " ", string name = "Workers Day")
        {
            return HolidayBuilder
                .Create()
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
                    RandomValuesGenerator.RandomString(10));

                holidays.Add(randomHoliday);
            }

            return holidays;
        }
    }
}
