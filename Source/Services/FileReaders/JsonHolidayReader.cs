using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.DTO;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// A class to read the holiday information from a Json file
    /// </summary>
    public class JsonHolidayReader : IJsonReader
    {
        public List<Holiday> Holidays { get; set; }

        public JsonHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{FileExtension.Json}"))
            {
                throw new InvalidOperationException($"File extension {FileExtension.Json} was expected");
            }

            return this.HolidaysFromJson(absoluteFilePath);
        }

        [ExcludeFromCodeCoverage]
        protected List<Holiday> HolidaysFromJson(string absoluteFilePath)
        {
            this.Holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(absoluteFilePath))
            {
                string json = file.ReadToEnd();
                var deserializedInfo = JsonConvert.DeserializeObject<HolidaysInfoList>(json);

                if (deserializedInfo == null) return this.Holidays;

                this.Holidays = deserializedInfo.Holidays;
                //in case its needed
                this.Holidays.ForEach(holiday => 
                    holiday.HolidayStringDate =
                        holiday.HolidayDate.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture));
            }
            return this.Holidays;
        }
    }
}
