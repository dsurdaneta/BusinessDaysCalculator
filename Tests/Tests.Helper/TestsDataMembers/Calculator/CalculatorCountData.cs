using System;
using System.Collections;
using System.Collections.Generic;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;

namespace DsuDev.BusinessDays.Tests.Helper.TestsDataMembers.Calculator
{
    public class CalculatorCountData : CalculatorBaseTestData, IEnumerable<object[]>
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
                var endDate = RandomEndDate(startDate);
                var holidays = HolidayGenerator.CreateRandomHolidays(amount, year);

                yield return new object[] { startDate, endDate, holidays, RandomValuesGenerator.RandomBoolean()};
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
