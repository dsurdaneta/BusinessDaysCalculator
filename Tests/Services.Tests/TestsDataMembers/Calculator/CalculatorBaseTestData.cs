using System;
using DsuDev.BusinessDays.Common.Tools;

namespace DsuDev.BusinessDays.Services.Tests.TestsDataMembers.Calculator
{
    public class CalculatorBaseTestData
    {
        internal const int BaseYear = 1986;
        internal const int MinAmountHolidays = 5;
        internal const int MaxAmountHolidays = 24;
        internal const int MinSamples = 5;
        internal const int MaxSamples = 15;
        
        internal static DateTime RandomStartDate(int year)
        {
            return new DateTime(
                year,
                RandomValuesGenerator.RandomInt(1, 12),
                RandomValuesGenerator.RandomInt(1, 12)
            );
        }

        internal static DateTime RandomEndDate(DateTime startDate)
        {
            var month = startDate.Month;

            if (startDate.Month < 10)
            {
                var maxMonth = startDate.Month + RandomValuesGenerator.RandomInt(2);
                month = RandomValuesGenerator.RandomInt(startDate.Month, maxMonth);
            }

            var minDay = startDate.Month < month ? 1 : 15;

            return new DateTime(startDate.Year, month, RandomValuesGenerator.RandomInt(minDay, 28));
        }
    }
}
