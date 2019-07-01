using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// Handles the file reading of the Holidays in different formats
    /// </summary>
    /// <seealso cref="IFileReadingManager" />
    public interface IFileReadingManager
    {
        IJsonReader JsonReader { get; }
        IXmlReader XmlReader { get; }
        ICsvReader CsvReader { get; }
        ICustomTxtReader CustomTxtReader { get; }

        /// <summary>
        /// Reads the holidays file.
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">File extension {filePathInfo.Extension}</exception>
        List<Holiday> ReadHolidaysFile(FilePathInfo filePathInfo);
    }
}
