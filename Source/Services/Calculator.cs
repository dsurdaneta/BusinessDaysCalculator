using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DsuDev.BusinessDays.Common.Extensions;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces;

namespace DsuDev.BusinessDays.Services
{
    /// <summary>
    /// Class to handle every calculation related with business days and/or holidays.
    /// </summary>
    public class Calculator : ICalculator
    {
        private readonly IMapper mapper;
        private readonly IDataProvider dataProvider;
        /// <remarks>Holiday only counts if is on a business day</remarks>
        private bool HolidayIsAWeekDay(Holiday holiday) => holiday.HolidayDate.IsAWeekDay();

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dataProvider">The data provider.</param>
        /// <exception cref="ArgumentNullException">
        /// mapper  or  dataProvider
        /// </exception>
        public Calculator(IMapper mapper, IDataProvider dataProvider)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        /// <inheritdoc />
        public double GetBusinessDaysCount(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                return 0;
            }

            //read holiday list and count them
            var holidays = this.dataProvider.GetHolidays(startDate, endDate);
            var holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);
            return this.AddCountersToDate(startDate, endDate, holidaysCount);
        }

        /// <inheritdoc />
        public double GetBusinessDaysCount(DateTime startDate, DateTime endDate, ICollection<Holiday> holidays)
        {
            if (holidays == null)
            {
                throw new ArgumentNullException(nameof(holidays));
            }

            //minus holidays
            int holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);
            return this.AddCountersToDate(startDate, endDate, holidaysCount);
        }

        /// <inheritdoc />
        public async Task<DateTime> AddBusinessDaysAsync(DateTime startDate, double daysCount)
        {
            if (daysCount <= 0) return startDate;
            
            //read holiday list
            var holidays = await this.dataProvider.GetAllHolidaysAsync().ConfigureAwait(false);

            //plus weekends
            int weekendCount = this.GetWeekendsCount(startDate, daysCount);
            DateTime endDate = startDate.AddDays(daysCount + weekendCount);

            //count the holidays an add them to the date
            int holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);
            if (holidaysCount > 0)
                endDate = endDate.AddDays(holidaysCount);

            return endDate;
        }

        /// <inheritdoc />
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount)
        {
            if (daysCount <= 0) return startDate;

            //plus weekends
            double fullDaysCount = daysCount + this.GetWeekendsCount(startDate, daysCount);
            if (notWeekendHolidaysCount > 0)
            {
                fullDaysCount += notWeekendHolidaysCount;
            }

            //add everything to the initial date
            return startDate.AddDays(fullDaysCount);
        }

        /// <inheritdoc />
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, ICollection<Holiday> holidays)
        {
            if (daysCount <= 0) return startDate;

            int notWeekendHolidaysCount = 0;
            if (holidays != null && holidays.Any())
            {
                //The holidays count must consider holidays since evaluation date
                bool HolidaySince(Holiday h) => h.HolidayDate.ToUniversalTime() >= startDate.ToUniversalTime();

                notWeekendHolidaysCount = holidays.AsParallel().Where(HolidaySince).Count(this.HolidayIsAWeekDay);
            }

            return this.AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);
        }

        /// <summary>
        /// Adds the counters to the given date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="holidaysCount">The amount of holidays.</param>
        /// <returns></returns>
        internal double AddCountersToDate(DateTime startDate, DateTime endDate, int holidaysCount)
        {
            //initial date difference calculation
            double result = (endDate.ToUniversalTime() - startDate.ToUniversalTime()).TotalDays;

            //minus weekends
            int weekendCount = this.GetWeekendsCount(startDate, result);
            result -= (weekendCount + holidaysCount);

            return result;
        }

        /// <summary>
        /// Gets the number of Saturdays and Sundays for a given number of business days
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <returns></returns>
        internal int GetWeekendsCount(DateTime startDate, double daysCount)
        {
            int weekendCount = 0;
            for (int i = 0; i < daysCount; i++)
            {
                DateTime calculatedDateTime = startDate.AddDays(i);
                if (calculatedDateTime.IsWeekend())
                    weekendCount++;
            }
            return weekendCount;
        }

        /// <summary>
        /// Gets the number of holidays between two dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns>The amount of Holidays</returns>
        internal int GetHolidaysCount(DateTime startDate, DateTime endDate, ICollection<Holiday> holidays)
        {
            int holidayCount = 0;
            if (holidays?.Count > 0)
            {
                //The holidays count must consider holidays between evaluation dates
                bool HolidayIsBetweenDates(Holiday holiday) => holiday.HolidayDate.ToUniversalTime() >= startDate.ToUniversalTime()
                                                               && holiday.HolidayDate.ToUniversalTime() <= endDate.ToUniversalTime();

                holidayCount = holidays.AsParallel().Where(HolidayIsBetweenDates).Count(this.HolidayIsAWeekDay);
            }
            return holidayCount;
        }
    }
}
