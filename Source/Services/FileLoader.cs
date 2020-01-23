using System;
using System.Collections.Generic;
using System.Text;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;

namespace DsuDev.BusinessDays.Services
{
    public class FileLoader : IFileLoader
    {
        private readonly IFileReadingManager fileReading;
        /// <inheritdoc />
        public FilePathInfo FilePathInfo { get; set; }

        /// <inheritdoc />
        public List<Holiday> Holidays { get; set; }

        public FileLoader(IFileReadingManager fileReadingManager, FilePathInfo filePathInfo)
        {
            this.FilePathInfo = filePathInfo ?? GetDefaultFilePathInfoValues();
            this.fileReading = fileReadingManager ?? throw new ArgumentNullException(nameof(fileReadingManager));
        }

        public FileLoader()
        {
            this.FilePathInfo = GetDefaultFilePathInfoValues();
            this.fileReading = new FileReadingManager();
        }

        /// <inheritdoc />
        public bool LoadFile(FilePathInfo filePathInfo)
        {
            var path = filePathInfo ?? this.FilePathInfo;
            try
            {
                this.Holidays = this.fileReading.ReadHolidaysFile(path);
                return this.Holidays.Count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public bool SaveHolidays()
        {
            throw new NotImplementedException();
        }

        private static FilePathInfo GetDefaultFilePathInfoValues()
        {
            return new FilePathInfo
            {
                Folder = Resources.ContainingFolderName,
                FileName = Resources.FileName,
                Extension = FileExtension.Json,
                IsAbsolutePath = false
            };
        }
    }
}
