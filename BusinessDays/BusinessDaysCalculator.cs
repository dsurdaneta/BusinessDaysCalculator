using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;
using CsvHelper;

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
            int holidaysCount = (readHolidaysFile) ? GetHolidaysCount(startDate, endDate, folder, fileName, fileExt) : 0;

            result -= (weekendCount + holidaysCount);

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
                    weekendCount++;                
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
            var currentDirectory = Directory.GetCurrentDirectory();
            string folderPath = $"{currentDirectory}\\{folder}";

            //if it does not exist, create directory
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fullFilePath = $"{folderPath}\\{fileName}.{fileExt}";

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
                    using (XmlReader file = XmlReader.Create(fullFilePath))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(file);

                        foreach(XmlNode node in xDoc.ChildNodes[1])
                        {
                            if (node.ChildNodes.Count < 3)                           
                                continue;

                            Holiday holidayInfo = new Holiday
                            {
                                HolidayDate = Convert.ToDateTime(node.ChildNodes[0].InnerText),
                                HolidayStringDate = node.ChildNodes[0].InnerText, //in case its needed
                                Name = node.ChildNodes[1].InnerText,
                                Description = node.ChildNodes[2].InnerText
                            };
                            holidays.Add(holidayInfo);
                        }
                    }                    
                    break;               
                case FileExtension.CSV:
                    using (StreamReader file = File.OpenText(fullFilePath))
                    {
                        var csv = new CsvReader(file);                        
                        csv.Configuration.HasHeaderRecord = true;
                        csv.Configuration.Delimiter = ";";

                        while (csv.Read())
                        {
                            Holiday holidayInfo = new Holiday
                            {
                                HolidayDate = csv.GetField<DateTime>(0),
                                HolidayStringDate = csv.GetField(0), //in case its needed
                                Name = csv.GetField<string>(1),
                                Description = csv.GetField(2)
                            };
                            holidays.Add(holidayInfo);
                        }
                    }
                    break;
                case FileExtension.TXT:
                    break;
                default:
                    //file extension is not supported
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
            Func<Holiday, bool> holidayIsBetweenDates = (h => h.HolidayDate >= startDate && h.HolidayDate <= endDate);

            foreach (Holiday holiday in holidays.Where(holidayIsBetweenDates))
            {
                //holiday only counts if is on a business day
                if ((holiday.HolidayDate.DayOfWeek > DayOfWeek.Sunday) && (holiday.HolidayDate.DayOfWeek < DayOfWeek.Saturday))               
                    holidayCount++;
            }

            return holidayCount;
        }
        #endregion
    }
}
