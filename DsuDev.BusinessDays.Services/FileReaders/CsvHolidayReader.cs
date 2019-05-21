using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public class CsvHolidayReader : IHolidayFileReader
    {
        private const string DefaultDelimiter = ";";
        private const int DateIndex = 0;
        private const int NameIndex = 1;
        private const int DescriptionIndex = 2;

        private readonly string delimiter;

        public List<Holiday> Holidays { get; set; }

        public CsvHolidayReader()
        {
            this.delimiter = DefaultDelimiter;
            this.Holidays = new List<Holiday>();
        }

        public CsvHolidayReader(string delimiter)
        {
            this.delimiter = string.IsNullOrWhiteSpace(delimiter)  ? DefaultDelimiter : delimiter;
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
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = this.delimiter;

                var holidayBuilder = new HolidayBuilder();
                while (csv.Read())
                {
                    holidayBuilder.Create()
                        .WithDate(csv.GetField<DateTime>(DateIndex))
                        .WithName(csv.GetField<string>(NameIndex))
                        .WithDescription(csv.GetField(DescriptionIndex));

                    this.Holidays.Add(holidayBuilder.Build());
                }
            }
            return this.Holidays;
        }

    }
}
