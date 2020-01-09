using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Common.Extensions;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DsuDev.BusinessDays.Services
{
    /// <summary>
    /// Class to handle every calculation related with business days and/or holidays
    /// </summary>
    public class BusinessDaysCalculator
    {
        private readonly IFileReadingManager fileReading; 
        //Holiday only counts if is on a business day
        private bool HolidayIsAWeekDay(Holiday holiday) => holiday.HolidayDate.IsAWeekDay();
        public FilePathInfo FilePathInfo { get; set; }       

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessDaysCalculator"/> class.
        /// </summary>
        public BusinessDaysCalculator()
        {
            this.FilePathInfo = new FilePathInfo
            {
                Folder = Resources.ContainingFolderName, 
                FileName = Resources.FileName, 
                Extension = FileExtension.Json,
                IsAbsolutePath = false
            };

            this.fileReading = new FileReadingManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessDaysCalculator"/> class.
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <param name="fileReadingManager">The file reading manager.</param>
        /// <exception cref="ArgumentNullException">
        /// filePathInfo  or  fileReadingManager
        /// </exception>
        public BusinessDaysCalculator(FilePathInfo filePathInfo, IFileReadingManager fileReadingManager)
        {
            this.FilePathInfo = filePathInfo ?? throw new ArgumentNullException(nameof(filePathInfo));
            this.fileReading = fileReadingManager ?? throw new ArgumentNullException(nameof(fileReadingManager));
        }

        /// <summary>
        /// Calculates the number of business days between two given dates
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="readHolidaysFile">if set to <c>true</c> [read holidays file].</param>
        /// <returns></returns>
        public double GetBusinessDaysCount(DateTime startDate, DateTime endDate, bool readHolidaysFile = false)
        {
            int holidaysCount = 0;
            if (readHolidaysFile)
            {
                DirectoryHelper.ValidateFilePathInfo(this.FilePathInfo);
                //minus holidays
                List<Holiday> holidays = this.fileReading.ReadHolidaysFile(this.FilePathInfo);
                holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);
            }

            return this.AddCountersToDate(startDate, endDate, holidaysCount);
        }

        /// <summary>
        /// Calculates the number of business days between two given dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
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
		
        /// <summary>
        /// Adds a specific number of business days
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="daysCount">business days to add</param>
        /// <param name="readHolidaysFile">if you have a file with the holidays</param>
        /// <returns></returns>
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, bool readHolidaysFile = false)
        {
            if(daysCount <= 0) return startDate;

            //plus weekends
            int weekendCount = this.GetWeekendsCount(startDate, daysCount);
            DateTime endDate = startDate.AddDays(daysCount + weekendCount);

            if (!readHolidaysFile) return endDate;
            
            //holidays calculation
            List<Holiday> holidays = this.fileReading.ReadHolidaysFile(this.FilePathInfo);
            int holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);
            
            //add the holidays to the date
            if(holidaysCount > 0)
                endDate = endDate.AddDays(holidaysCount);
            
            return endDate;
        }

        /// <summary>
        /// Adds a specific number of business days, considering also a number of holidays
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <param name="notWeekendHolidaysCount">if you already have the holidays count</param>
        /// <returns></returns>
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount)
        {
            if(daysCount <= 0) return startDate;

            //plus weekends
            double fullDaysCount = daysCount + this.GetWeekendsCount(startDate, daysCount);
            if (notWeekendHolidaysCount > 0)
            {
                fullDaysCount += notWeekendHolidaysCount;
            }

            //add everything to the initial date
            return startDate.AddDays(fullDaysCount);
        }

        /// <summary>
        /// Adds a specific number of business days, considering also a number of holidays
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, ICollection<Holiday> holidays)
        {
            if(daysCount <= 0) return startDate;

            int notWeekendHolidaysCount = 0;
            if (holidays != null && holidays.Any())
            {
                //The holidays count must consider holidays since evaluation date
                bool HolidaySince(Holiday h) => h.HolidayDate >= startDate;

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
            double result = (endDate - startDate).TotalDays;
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
                bool HolidayIsBetweenDates(Holiday holiday) => holiday.HolidayDate >= startDate 
                                                               && holiday.HolidayDate <= endDate;

                holidayCount = holidays.AsParallel().Where(HolidayIsBetweenDates).Count(this.HolidayIsAWeekDay);
            }
            return holidayCount;
        }
    }
}
