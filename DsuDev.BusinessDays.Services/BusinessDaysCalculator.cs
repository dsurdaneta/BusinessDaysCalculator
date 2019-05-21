using System;
using System.Collections.Generic;
using System.Linq;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Services.FileReaders;

namespace DsuDev.BusinessDays.Services
{
    /// <summary>
    /// Class to handle every calculation related with business days and/or holidays
    /// </summary>
    public class BusinessDaysCalculator
    {
        private readonly IFileReadingManager fileReading;
        public FilePathInfo FilePathInfo { get; set; }

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
        
        public BusinessDaysCalculator(FilePathInfo filePathInfo, IFileReadingManager fileReadingManager)
        {
            this.FilePathInfo = filePathInfo ?? throw new ArgumentNullException(nameof(filePathInfo));
            this.fileReading = fileReadingManager ?? throw new ArgumentException(nameof(fileReadingManager));
        }

        //TODO: change file properties for the FileInfoPath Entity
        
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

            return AddCountersToDate(startDate, endDate, holidaysCount);
        }

        /// <summary>
        /// Calculates the number of business days between two given dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
        public double GetBusinessDaysCount(DateTime startDate, DateTime endDate, List<Holiday> holidays)
        {
            if (holidays == null)
            {
                throw new ArgumentNullException(nameof(holidays));
            }

            //minus holidays
            int holidaysCount = this.GetHolidaysCount(startDate, endDate, holidays);

            return AddCountersToDate(startDate, endDate, holidaysCount);
        }
				
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
        /// Adds a specific number of business days
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="daysCount">business days to add</param>
        /// <param name="readHolidaysFile">if you have a file with the holidays</param>
        /// <returns></returns>
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, bool readHolidaysFile = false)
        {
            //plus weekends
            int weekendCount = this.GetWeekendsCount(startDate, daysCount);
            //add everything to the initial date
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
            //plus weekends
            int weekendCount = this.GetWeekendsCount(startDate, daysCount);
            //add everything to the initial date
            return startDate.AddDays(daysCount + weekendCount + notWeekendHolidaysCount);
        }

        /// <summary>
        /// Adds a specific number of business days, considering also a number of holidays
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
        public DateTime AddBusinessDays(DateTime startDate, double daysCount, List<Holiday> holidays)
        {
            int notWeekendHolidaysCount = this.GetHolidaysCount(startDate, holidays);
            return this.AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);
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
                DateTime calcDateTime = startDate.AddDays(i);
                if (calcDateTime.DayOfWeek == DayOfWeek.Saturday || calcDateTime.DayOfWeek == DayOfWeek.Sunday)               
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
        internal int GetHolidaysCount(DateTime startDate, DateTime endDate, List<Holiday> holidays)
        {
            int holidayCount = 0;
            if (holidays != null && holidays.Count > 0)
            {
                //The holidays count must consider holidays between evaluation dates
                bool HolidayIsBetweenDates(Holiday holiday) => holiday.HolidayDate >= startDate 
                                                               && holiday.HolidayDate <= endDate;
                //Holiday only counts if is on a business day
                bool HolidayIsAWeekDay(Holiday holiday) => holiday.HolidayDate.DayOfWeek > DayOfWeek.Sunday 
                                                           && holiday.HolidayDate.DayOfWeek < DayOfWeek.Saturday;

                holidayCount = holidays.Where(HolidayIsBetweenDates).Count(HolidayIsAWeekDay);
            }
            return holidayCount;
        }

        /// <summary>
        /// Gets the number of holidays since a specific day.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns>The amount of Holidays</returns>
        internal int GetHolidaysCount(DateTime startDate, List<Holiday> holidays)
        {
            int holidayCount = 0;
            if (holidays != null && holidays.Count > 0)
            {
                //The holidays count must consider holidays since evaluation date
                bool HolidaySince(Holiday h) => h.HolidayDate >= startDate;
                //Holiday only counts if is on a business day
                bool IsAWeekDay(Holiday h) => h.HolidayDate.DayOfWeek > DayOfWeek.Sunday 
                                              && h.HolidayDate.DayOfWeek < DayOfWeek.Saturday;

                holidayCount = holidays.Where(HolidaySince).Count(IsAWeekDay);
            }
            return holidayCount;
        }
    }
}
