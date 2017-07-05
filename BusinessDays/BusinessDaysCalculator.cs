﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace DsuDev.BusinessDays
{
    /// <summary>
    /// Class to handle every calculation related with business days and/or holidays
    /// </summary>
    public class BusinessDaysCalculator
    {
        #region Constants
        internal const string ResourceFolder = "Resources";
        internal const string FileName = "holidays";
        #endregion

        #region static Methods
        /// <summary>
        /// Calculates the number of business days between two given dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="readHolidaysFile"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
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
        /// Adds a specific number of business days
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount">business days to add</param>
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
        /// Adds a specific number of business days, considering also a number of holidays
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

        /// <summary>
        /// Gets the number of Saturdays and Sundays for a given number of business days
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="daysCount"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Process a file with the holidays and saves it in a List
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        internal static List<Holiday> ReadHolidaysFile(string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            List<Holiday> holidays = new List<Holiday>();
            //if it does not exist, create directory            
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string folderPath = $"{currentDirectory}/{folder}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullFilePath = $"{currentDirectory}/{folder}/{fileName}.{fileExt}";

            //format to list according to fileExt
            switch (fileExt)
            {
                case FileExtension.JSON:
                    using (StreamReader file = File.OpenText(fullFilePath))
                    {
                        string json = file.ReadToEnd();
                        var deserializedInfo = JsonConvert.DeserializeObject<HolidaysInfoList>(json);

                        if (deserializedInfo != null)
                            holidays = deserializedInfo.Holidays;
                    }
                    break;
                case FileExtension.XML:
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(fullFilePath);
                    //Deserializing the Xml
                    using (StreamReader file = File.OpenText(fullFilePath))
                    {
                        //XmlSerializer ser = new XmlSerializer(typeof(HolidaysInfoList));
                    }

                    break;
                case FileExtension.TXT:
                    break;
                case FileExtension.CSV:
                    break;
                default:
                    //file extension is not supported;
                    return holidays;
            }

            return holidays;
        }
       
        /// <summary>
        /// Gets the number of holidays between two dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        internal static int GetHolidaysCount(DateTime startDate, DateTime endDate, string folder = ResourceFolder, string fileName = FileName, string fileExt = FileExtension.JSON)
        {
            //The holidays count must consider holidays between evaluation dates
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
