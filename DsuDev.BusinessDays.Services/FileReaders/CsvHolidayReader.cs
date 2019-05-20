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
        private static readonly int DateIndex = 0;
        private static readonly int NameIndex = 1;
        private static readonly int DescriptionIndex = 2;

        private static readonly string Delimiter = ";";
        public List<Holiday> HolidaysFromFile(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{FileExtension.Csv}"))
            {
                throw new InvalidOperationException($"File extension {FileExtension.Csv} was expected");
            }

            return HolidaysFromCsv(absoluteFilePath);
        }

        protected static List<Holiday> HolidaysFromCsv(string fullFilePath)
        {
            List<Holiday> holidays = new List<Holiday>();
            using (StreamReader file = File.OpenText(fullFilePath))
            {
                var csv = new CsvReader(file);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = Delimiter;

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
    }
}
