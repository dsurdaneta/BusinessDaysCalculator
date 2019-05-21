using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using CsvHelper;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Services
{
    /// <summary>
    /// Class to handle every calculation related with business days and/or holidays
    /// </summary>
    public class BusinessDaysCalculator
    {
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
        public static double GetBusinessDaysCount(
            DateTime startDate, 
            DateTime endDate, 
            bool readHolidaysFile = false, 
            string folder = Resources.ContainingFolderName, 
            string fileName = Resources.FileName, 
            string fileExt = FileExtension.Json)
        {			
            //minus holidays
            int holidaysCount = readHolidaysFile ? 
                GetHolidaysCount(startDate, endDate, folder, fileName, fileExt) 
                : 0;

            return AddCountersToDate(startDate, endDate, holidaysCount);
        }

        /// <summary>
        /// Calculates the number of business days between two given dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
        public static double GetBusinessDaysCount(DateTime startDate, DateTime endDate, List<Holiday> holidays)
        {
            if (holidays == null)
            {
                throw new ArgumentNullException(nameof(holidays));
            }

            //minus holidays
            int holidaysCount = GetHolidaysCount(startDate, endDate, holidays);

            return AddCountersToDate(startDate, endDate, holidaysCount);
        }
				
        internal static double AddCountersToDate(DateTime startDate, DateTime endDate, int holidaysCount)
        {
            //initial date difference calculation
            double result = (endDate - startDate).TotalDays;
            //minus weekends
            int weekendCount = GetWeekendsCount(startDate, result);
			
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
        public static DateTime AddBusinessDays(
            DateTime startDate, 
            double daysCount, 
            bool readHolidaysFile = false,
            string folder = Resources.ContainingFolderName, 
            string fileName = Resources.FileName, 
            string fileExt = FileExtension.Json)
        {
            //plus weekends
            int weekendCount = GetWeekendsCount(startDate, daysCount);
            //add everything to the initial date
            DateTime endDate = startDate.AddDays(daysCount + weekendCount);

            if (!readHolidaysFile) return endDate;
            //holidays calculation
            int holidaysCount = GetHolidaysCount(startDate, endDate, folder, fileName, fileExt);
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
        public static DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount)
        {
            //plus weekends
            int weekendCount = GetWeekendsCount(startDate, daysCount);
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
        public static DateTime AddBusinessDays(DateTime startDate, double daysCount, List<Holiday> holidays)
        {
            int notWeekendHolidaysCount = GetHolidaysCount(startDate, holidays);
            return AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);
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
                if (calcDateTime.DayOfWeek == DayOfWeek.Saturday || calcDateTime.DayOfWeek == DayOfWeek.Sunday)               
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
        internal static List<Holiday> ReadHolidaysFile(
            string folder = Resources.ContainingFolderName,
            string fileName = Resources.FileName, 
            string fileExt = FileExtension.Json)
        {
            List<Holiday> holidays = new List<Holiday>();
            string fullFilePath = GenerateFilePath(folder, fileName, fileExt);
            //format to list according to fileExt
            switch (fileExt)
            {
                case FileExtension.Json:
                    holidays = HolidaysFromJson(fullFilePath);
                    break;
                case FileExtension.Xml:
                    holidays = HolidaysFromXml(fullFilePath);
                    break;
                case FileExtension.Csv:
                    holidays = HolidaysFromCsv(fullFilePath);
                    break;
                case FileExtension.Txt:
                    //not yet supported, might be useful for custom rules
                    break;
                default:
                    //file extension is not supported
                    throw new InvalidOperationException($"File extension {fileExt} not supported");
            }
            return holidays;
        }

        protected static string GenerateFilePath(
            string folder = Resources.ContainingFolderName, 
            string fileName = Resources.FileName, 
            string fileExt = FileExtension.Json)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folderPath = $"{currentDirectory}\\{folder}";

            //if it does not exist, create directory
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return $"{folderPath}\\{fileName}.{fileExt}";
        }

        protected static List<Holiday> HolidaysFromCsv(string fullFilePath)
        {
            List<Holiday> holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(fullFilePath))
            {
                var csv = new CsvReader(file);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ";";
                var holidayBuilder = new HolidayBuilder();
				
                while (csv.Read())
                {
                    holidayBuilder.Create()
                        .WithDate(csv.GetField<DateTime>(0))
                        .WithName(csv.GetField<string>(1))
                        .WithDescription(csv.GetField(2));

                    holidays.Add(holidayBuilder.Build());
                }
            }
            return holidays;
        }

        protected static List<Holiday> HolidaysFromXml(string fullFilePath)
        {
            List<Holiday> holidays = new List<Holiday>();
            using (XmlReader file = XmlReader.Create(fullFilePath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                var holidayBuilder = new HolidayBuilder();
                foreach (XmlNode node in xDoc.ChildNodes[1])
                {
                    if (node.ChildNodes.Count < 3)
                        continue;

                    holidayBuilder.Create()
                        .WithDate(Convert.ToDateTime(node.ChildNodes[0].InnerText))
                        .WithName(node.ChildNodes[1].InnerText)
                        .WithDescription(node.ChildNodes[2].InnerText);

                    holidays.Add(holidayBuilder.Build());
                }
            }
            return holidays;
        }

        protected static List<Holiday> HolidaysFromJson(string fullFilePath)
        {
            List<Holiday> holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(fullFilePath))
            {
                string json = file.ReadToEnd();
                var deserializedInfo = JsonConvert.DeserializeObject<HolidaysInfoList>(json);

                if (deserializedInfo == null) return holidays;

                holidays = deserializedInfo.Holidays;
                //in case its needed
                holidays.ForEach(holiday => holiday.HolidayStringDate = holiday.HolidayDate.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture));
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
        internal static int GetHolidaysCount(
            DateTime startDate, DateTime endDate, 
            string folder = Resources.ContainingFolderName, 
            string fileName = Resources.FileName, 
            string fileExt = FileExtension.Json)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentException("FolderName should not be empty", nameof(folder) );
            }
            
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("FileName should not be empty", nameof(fileName) );
            }
            
            if (string.IsNullOrWhiteSpace(fileExt))
            {
                throw new ArgumentException("fileExtension should not be empty", nameof(fileExt) );
            }

            List<Holiday> holidays = ReadHolidaysFile(folder, fileName, fileExt);
            return GetHolidaysCount(startDate, endDate, holidays);
        }

        /// <summary>
        /// Gets the number of holidays between two dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidays">Holiday object list</param>
        /// <returns></returns>
        internal static int GetHolidaysCount(DateTime startDate, DateTime endDate, List<Holiday> holidays)
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
        /// <returns></returns>
        internal static int GetHolidaysCount(DateTime startDate, List<Holiday> holidays)
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
        #endregion
    }
}
