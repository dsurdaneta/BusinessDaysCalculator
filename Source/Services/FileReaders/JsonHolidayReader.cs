using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
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
    public class JsonHolidayReader : FileReaderBase, IJsonReader
    {
        public List<Holiday> Holidays { get; set; }

        public JsonHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            ValidatePath(absoluteFilePath, FileExtension.Json);

            return this.ReadHolidaysFromFile(absoluteFilePath);
        }
        
        public async Task<List<Holiday>> GetHolidaysFromFileAsync(string absoluteFilePath)
        {
            ValidatePath(absoluteFilePath, FileExtension.Json);

            this.Holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(absoluteFilePath))
            {
                string json = await file.ReadToEndAsync();
                var deserializedInfo = JsonConvert.DeserializeObject<HolidaysInfoList>(json);

                if (deserializedInfo == null) return this.Holidays;

                this.Holidays = deserializedInfo.Holidays;
                //in case its needed
                Parallel.ForEach(this.Holidays, holiday =>
                    holiday.HolidayStringDate =
                        holiday.HolidayDate.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture));
            }
            return this.Holidays;

        }
        
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override List<Holiday> ReadHolidaysFromFile(string absoluteFilePath)
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
