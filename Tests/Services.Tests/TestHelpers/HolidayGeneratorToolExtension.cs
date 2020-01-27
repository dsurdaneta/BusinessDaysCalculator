using System;
using System.Collections.Generic;
using System.Text;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Tests.TestHelpers
{
    public static class HolidayGeneratorToolExtension
    {
        public static DbModels.Holiday CreateDbHoliday(this HolidayGenerator generator, int holidayId, int year = 2001, int month = 5, int day = 1, string description = " ", string name = "Workers Day")
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

        public static List<DbModels.Holiday> CreateDbHolidays(this HolidayGenerator generator, int amount, int baseYear)
        {
            var holidays = new List<DbModels.Holiday>();

            for (int i = 0; i < amount; i++)
            {
                var randomHoliday = CreateDbHoliday(
                    generator,
                    RandomValuesGenerator.RandomInt(amount*5),
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
