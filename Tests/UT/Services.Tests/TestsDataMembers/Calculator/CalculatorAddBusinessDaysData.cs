using System;
using System.Collections;
using System.Collections.Generic;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;

namespace DsuDev.BusinessDays.Services.Tests.TestsDataMembers.Calculator
{
    public class CalculatorAddBusinessDaysData : CalculatorBaseTestData, IEnumerable<object[]>
    {
        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            var totalSamples = RandomValuesGenerator.RandomInt(MinSamples, MaxSamples);

            for (int i = 0; i < totalSamples; i++)
            {
                var year = RandomValuesGenerator.RandomInt(BaseYear, DateTime.Today.Year);
                var amount = RandomValuesGenerator.RandomInt(MinAmountHolidays, MaxAmountHolidays);

                var startDate = RandomStartDate(year);
                double daysCount = RandomValuesGenerator.RandomInt(MinSamples, 10);
                var useHolidayCollection = RandomValuesGenerator.RandomBoolean();
                var holidays = useHolidayCollection ? HolidayGenerator.CreateRandomHolidays(amount, year) : null;
                double notWeekendHolidaysCount = useHolidayCollection ? 0 : RandomValuesGenerator.RandomInt(MinSamples);

                yield return new object[] { startDate, daysCount, useHolidayCollection, notWeekendHolidaysCount, holidays };
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
