using CsvHelper;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using DsuDev.BusinessDays.Tools.FluentBuilders;
using System;
using System.Collections.Generic;
using System.IO;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// A class to read the holiday information from a CSV file
    /// </summary>
    public class CsvHolidayReader : ICsvHolidayReader
    {
        private const string DefaultDelimiter = ";";
        public List<Holiday> Holidays { get; set; }
        public string Delimiter { get; internal set; }
        public bool HasHeaderRecord { get; set; }

        public CsvHolidayReader() : this(true)
        {
            
        }

        public CsvHolidayReader(bool hasHeaderRecord, string delimiter = "")
        {
            this.Delimiter = string.IsNullOrWhiteSpace(delimiter) ? DefaultDelimiter : delimiter;
            this.HasHeaderRecord = hasHeaderRecord;
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{FileExtension.Csv}"))
            {
                throw new InvalidOperationException($"File extension {FileExtension.Csv} was expected");
            }

            return this.HolidaysFromCsv(absoluteFilePath);
        }

        protected List<Holiday> HolidaysFromCsv(string absoluteFilePath)
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
