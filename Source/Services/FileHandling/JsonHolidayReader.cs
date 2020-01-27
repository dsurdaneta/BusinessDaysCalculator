using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.DTO;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Services.FileHandling
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

        /// <summary>
        /// Gets the holidays from the specified file.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <returns></returns>
        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            ValidatePath(absoluteFilePath, FileExtension.Json);

            return this.ReadHolidaysFromFile(absoluteFilePath);
        }

        /// <summary>
        /// Gets the holidays from the specified file asynchronously.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <returns></returns>
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
                Parallel.ForEach(this.Holidays, FormatHolidayDate);
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
                foreach (var holiday in Holidays)
                {
                    FormatHolidayDate(holiday);
                }
            }
            return this.Holidays;
        }

        /// <summary>
        /// Formats the holiday: Removes the Time part, and asign the correct string format to HolidayStringDate value.
        /// </summary>
        /// <param name="holiday">The holiday.</param>
        private static void FormatHolidayDate(Holiday holiday)
        {
            holiday.HolidayDate = holiday.HolidayDate.Date;
            holiday.HolidayStringDate =
                holiday.HolidayDate.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture);
        }
    }
}
