using DsuDev.BusinessDays.Common.Tools;
using System;
using System.Collections.Generic;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Services.Tests.TestHelpers
{
    public static class HolidayGeneratorToolExtension
    {
        public static DbModels.Holiday CreateDbHoliday(int holidayId, int year = 2001, int month = 5, int day = 1, string description = " ", string name = "Workers Day")
        {
            return new DbModels.Holiday
            {
                CreatedDate = DateTime.UtcNow.AddDays(-5),
                UpdatedDate = DateTime.UtcNow.AddDays(-1),
                Id = holidayId,
                Name = name,
                Description = description,
                HolidayDate = new DateTime(year, month, day),
                Year = year
            };
        }

        public static List<DbModels.Holiday> CreateRandomDbHolidays(int amount, int baseYear, int baseId = 0)
        {
            var holidays = new List<DbModels.Holiday>();

            for (int i = 0; i < amount; i++)
            {
                var randomHoliday = CreateDbHoliday(
                    baseId + RandomValuesGenerator.RandomInt(1, amount*3),
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
