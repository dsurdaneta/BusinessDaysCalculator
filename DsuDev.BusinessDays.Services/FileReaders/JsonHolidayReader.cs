using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public class JsonHolidayReader : IHolidayFileReader
    {
        public List<Holiday> HolidaysFromFile(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{FileExtension.Json}"))
            {
                throw new InvalidOperationException($"File extension {FileExtension.Json} was expected");
            }

            return HolidaysFromJson(absoluteFilePath);
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
                holidays.ForEach(holiday => 
                    holiday.HolidayStringDate =
                        holiday.HolidayDate.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture));
            }
            return holidays;
        }
    }
}
