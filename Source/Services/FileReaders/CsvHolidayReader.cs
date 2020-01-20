using CsvHelper;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using DsuDev.BusinessDays.Common.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// A class to read the holiday information from a CSV file
    /// </summary>
    public class CsvHolidayReader : FileReaderBase, ICsvHolidayReader
    {
        private const string DefaultDelimiter = ";";
        public List<Holiday> Holidays { get; set; }
        public string Delimiter { get; internal set; }
        public bool HasHeaderRecord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHolidayReader"/> class.
        /// </summary>
        public CsvHolidayReader() : this(true)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHolidayReader"/> class.
        /// </summary>
        /// <param name="hasHeaderRecord">if set to <c>true</c> [has header record].</param>
        /// <param name="delimiter">The delimiter.</param>
        public CsvHolidayReader(bool hasHeaderRecord, string delimiter = "")
        {
            this.Delimiter = string.IsNullOrWhiteSpace(delimiter) ? DefaultDelimiter : delimiter;
            this.HasHeaderRecord = hasHeaderRecord;
            this.Holidays = new List<Holiday>();
        }

        /// <inheritdoc />
        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
           ValidatePath(absoluteFilePath, FileExtension.Csv);

            return this.ReadHolidaysFromFile(absoluteFilePath);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override List<Holiday> ReadHolidaysFromFile(string absoluteFilePath)
        {
            this.Holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(absoluteFilePath))
            {
                var csv = new CsvReader(file);
                csv.Configuration.HasHeaderRecord = this.HasHeaderRecord;
                csv.Configuration.Delimiter = this.Delimiter;
                
                var holidayBuilder = new HolidayBuilder();
                while (csv.Read())
                {
                    holidayBuilder.Create()
                        .WithDate(csv.GetField<DateTime>(FieldIndex.Date))
                        .WithName(csv.GetField<string>(FieldIndex.Name))
                        .WithDescription(csv.GetField(FieldIndex.Description));

                    this.Holidays.Add(holidayBuilder.Build());
                }
                csv.Dispose();
            }
            return this.Holidays;
        }

    }
}
