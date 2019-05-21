using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    internal class FileReadingManager : IFileReadingManager
    {
        private readonly JsonHolidayReader jsonReader;
        private readonly XmlHolidayReader xmlReader;
        private readonly CsvHolidayReader CsvReader;
        private readonly CustomTxtHolidayReader CustomTxtReader;

        public FileReadingManager()
        {
            this.jsonReader = new JsonHolidayReader();
            this.xmlReader = new XmlHolidayReader();
            this.CsvReader = new CsvHolidayReader();
            this.CustomTxtReader = new CustomTxtHolidayReader();
        }

        /// <summary>
        /// Process a file with the holidays and saves it in a List
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">File extension {fileExt}</exception>
        public List<Holiday> ReadHolidaysFile(FilePathInfo filePathInfo)
        {
            string path = DirectoryHelper.GenerateFilePath(filePathInfo);
            List<Holiday> holidays = new List<Holiday>();
            
            //format to list according to fileExt
            switch (filePathInfo.Extension)
            {
                case FileExtension.Json:
                    holidays = this.jsonReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Xml:
                    holidays = this.xmlReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Csv:
                    holidays = this.CsvReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Txt:
                    //not yet supported, might be useful for custom rules
                    this.CustomTxtReader.GetHolidaysFromFile(path);
                    break;
                default:
                    //file extension is not supported
                    throw new InvalidOperationException($"File extension {filePathInfo.Extension} not supported");
            }

            return holidays;
        }
    }
}
