using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessDays
{
    /// <summary>
    /// Class to handle every calculation related with business days or holidays
    /// </summary>
    public class BusinessDaysCalculator
    {
        #region Constants
        internal const string ResourceFolder = "Resources";
        internal const string FileName = "holidays";
        #endregion

        #region static Methods
        public static double GetBusinessDaysCount(DateTime startDate, DateTime endDate, bool readHolidays = false, string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //initial date difference calculation
            double result = (endDate - startDate).TotalDays;

            //minus weekends
            int weekendCount = GetWeekendsCount(startDate, result);
            //minus holidays
            int holidaysCount = (readHolidays) ? GetHolidaysCount(folder, fileName) : 0;

            result = result - weekendCount - holidaysCount;

            return result;
        }
       
        public static DateTime AddBusinessDays(DateTime startDate, double daysCount, bool readHolidays = false,
            string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //plus weekends
            int weekendCount = GetWeekendsCount(startDate, daysCount);
            //plus holidays
            int holidaysCount = (readHolidays) ? GetHolidaysCount(folder, fileName, fileExt) : 0;
            //add everything to the initial date
            DateTime endDate = startDate.AddDays(daysCount + weekendCount + holidaysCount);

            return endDate;
        }

        internal static int GetWeekendsCount(DateTime startDate, double daysCount)
        {
            int weekendCount = 0;

            for (int i = 0; i < daysCount; i++)
            {
                DateTime calcDateTime = startDate.AddDays(1);
                if ((calcDateTime.DayOfWeek == DayOfWeek.Saturday) || (calcDateTime.DayOfWeek == DayOfWeek.Sunday))
                {
                    weekendCount++;
                }
            }
            return weekendCount;
        }

        internal static List<Holiday> ReadHolidaysFile(string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            List<Holiday> holidays = new List<Holiday>();
            switch (fileExt)
            {
                case FileExtension.JSON:
                    break;
                case FileExtension.XML:
                    break;
                case FileExtension.TXT:
                    break;
                default:
                    //file extension is not supported;
                    return holidays;
            }

            //TODO
            throw new NotImplementedException();
        }

        internal static int GetHolidaysCount(string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            int holidayCount = 0;
            List<Holiday> holidays = ReadHolidaysFile(folder, fileName, fileExt);

            foreach (Holiday holiday in holidays)
            {
                //holiday only counts if is on a business day
                if ((holiday.HolidayDate.DayOfWeek > DayOfWeek.Sunday) && (holiday.HolidayDate.DayOfWeek < DayOfWeek.Saturday))
                {
                    holidayCount++;
                }
            }

            return holidayCount;
        }
        #endregion
    }
}
