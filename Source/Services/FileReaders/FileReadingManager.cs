using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.Constants;

[assembly: InternalsVisibleTo("DsuDev.BusinessDays.Services.Tests")]
namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// Handles the file reading of the Holidays in different formats
    /// </summary>
    /// <seealso cref="IFileReadingManager" />
    internal class FileReadingManager : IFileReadingManager
    {
        public IJsonReader JsonReader { get; }
        public IXmlReader XmlReader { get; }
        public ICsvReader CsvReader { get; }
        public ICustomTxtReader CustomTxtReader { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReadingManager"/> class.
        /// </summary>
        public FileReadingManager()
        {
            this.JsonReader = new JsonHolidayReader();
            this.XmlReader = new XmlHolidayReader();
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
        /// <exception cref="ArgumentNullException">
        /// jsonReader  or  xmlReader  or  csvReader  or  customReader
        /// </exception>
        public FileReadingManager(
            IJsonReader jsonReader, 
            IXmlReader xmlReader, 
            ICsvReader csvReader, 
            ICustomTxtReader customReader)
        {
            this.JsonReader = jsonReader ?? throw new ArgumentNullException(nameof(jsonReader));
            this.XmlReader = xmlReader ?? throw new ArgumentNullException(nameof(xmlReader));
            this.CsvReader = csvReader ?? throw new ArgumentNullException(nameof(csvReader));
            this.CustomTxtReader = customReader ?? throw new ArgumentNullException(nameof(customReader));
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
            List<Holiday> holidays;
            
            //format to list according to fileExt
            switch (filePathInfo.Extension)
            {
                case FileExtension.Json:
                    holidays = this.JsonReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Xml:
                    holidays = this.XmlReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Csv:
                    holidays = this.CsvReader.GetHolidaysFromFile(path);
                    break;
                case FileExtension.Txt:
                    //not yet supported, might be useful for custom rules
                    holidays = this.CustomTxtReader.GetHolidaysFromFile(path);
                    break;
                default:
                    throw new InvalidOperationException($"File extension {filePathInfo.Extension} is not supported");
            }

            return holidays;
        }
    }
}
