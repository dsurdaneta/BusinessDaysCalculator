using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DsuDev.BusinessDays
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
        public static double GetBusinessDaysCount(DateTime startDate, DateTime endDate, bool readHolidaysFile = false, string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //initial date difference calculation
            double result = (endDate - startDate).TotalDays;

            //minus weekends
            int weekendCount = GetWeekendsCount(startDate, result);
            //minus holidays
            int holidaysCount = (readHolidaysFile) ? GetHolidaysCount(startDate, endDate, folder, fileName) : 0;

            result = result - weekendCount - holidaysCount;

            return result;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <param name="readHolidaysFile">if you have a file with the holidays</param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static DateTime AddBusinessDays(DateTime startDate, double daysCount, bool readHolidaysFile = false,
            string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //plus weekends
            int weekendCount = GetWeekendsCount(startDate, daysCount);
            //add everything to the initial date
            DateTime endDate = startDate.AddDays(daysCount + weekendCount);
            //plus holidays
            int holidaysCount = (readHolidaysFile) ? GetHolidaysCount(startDate, endDate, folder, fileName, fileExt) : 0;
            //also add the holidays
            endDate = endDate.AddDays(holidaysCount);

            return endDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <param name="notWeekendHolidaysCount">if you already have the holidays count</param>
        /// <returns></returns>
        public static DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount)
        {
            //plus weekends
            int weekendCount = GetWeekendsCount(startDate, daysCount);
            //add everything to the initial date
            return startDate.AddDays(daysCount + weekendCount + notWeekendHolidaysCount);
        }

        internal static int GetWeekendsCount(DateTime startDate, double daysCount)
        {
            int weekendCount = 0;

            for (int i = 0; i < daysCount; i++)
            {
                DateTime calcDateTime = startDate.AddDays(i);
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
            //if it does not exist, create directory
            //check for the full path
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            
            if (!Directory.Exists($"{currentDirectory} /{folder}"))
            {
                Directory.CreateDirectory($"{currentDirectory}/{folder}");
            }
            
            //format to list according to fileExt
            switch (fileExt)
            {
                case FileExtension.JSON:
                    using (StreamReader file = File.OpenText($"{currentDirectory}/{folder}/{fileName}.{fileExt}"))
                    {
                        string json = file.ReadToEnd();
                        var deserializedInfo = JsonConvert.DeserializeObject<HolidaysInfoList>(json);

                        if (deserializedInfo != null)
                            holidays = deserializedInfo.Holidays;
                    }
                    break;
                case FileExtension.XML:
                    break;
                case FileExtension.TXT:
                    break;
                default:
                    //file extension is not supported;
                    return holidays;
            }

            return holidays;
        }
       
        internal static int GetHolidaysCount(DateTime startDate, DateTime endDate, string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //TODO: The holidays count must consider holidays between evaluation dates
            int holidayCount = 0;
            List<Holiday> holidays = ReadHolidaysFile(folder, fileName, fileExt);

            foreach (Holiday holiday in holidays.Where(h => h.HolidayDate >= startDate && h.HolidayDate <= endDate))
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
