﻿using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// Handles the file reading of the Holidays in different formats
    /// </summary>
    /// <seealso cref="IFileReadingManager" />
    internal class FileReadingManager : IFileReadingManager
    {
        public IJsonReader jsonReader { get; }
        public IXmlReader xmlReader { get; }
        public ICsvReader CsvReader { get; }
        public ICustomTxtReader CustomTxtReader { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReadingManager"/> class.
        /// </summary>
        public FileReadingManager()
        {
            this.jsonReader = new JsonHolidayReader();
            this.xmlReader = new XmlHolidayReader();
            this.CsvReader = new CsvHolidayReader();
            this.CustomTxtReader = new CustomTxtHolidayReader();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReadingManager"/> class.
        /// </summary>
        /// <param name="jsonReader">The json reader.</param>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="csvReader">The CSV reader.</param>
        /// <param name="customReader">The custom reader.</param>
        /// <exception cref="ArgumentException">
        /// jsonReader  or  xmlReader  or  csvReader  or  customReader
        /// </exception>
        public FileReadingManager(
            IJsonReader jsonReader, 
            IXmlReader xmlReader, 
            ICsvReader csvReader, 
            ICustomTxtReader customReader)
        {
            this.jsonReader = jsonReader ?? throw new ArgumentException(nameof(jsonReader));
            this.xmlReader = xmlReader ?? throw new ArgumentException(nameof(xmlReader));
            this.CsvReader = csvReader ?? throw new ArgumentException(nameof(csvReader));
            this.CustomTxtReader = customReader ?? throw new ArgumentException(nameof(customReader));
        }

        /// <summary>
        /// Process a file with the holidays and saves it in a List
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">File extension {filePathInfo.Extension}</exception>
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
                    throw new InvalidOperationException($"File extension {filePathInfo.Extension} is not supported");
            }

            return holidays;
        }
    }
}