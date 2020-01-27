using System;
using System.Collections.Generic;
using AutoMapper;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;

namespace DsuDev.BusinessDays.Services
{
    public class FileLoader : IFileLoader
    {
        private readonly IMapper mapper;
        private readonly IFileReadingManager fileReading;
        /// <inheritdoc />
        public FilePathInfo FilePathInfo { get; set; }

        /// <inheritdoc />
        public List<Holiday> Holidays { get; set; }

        public FileLoader(IMapper mapper, IFileReadingManager fileReadingManager, FilePathInfo filePathInfo)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.fileReading = fileReadingManager ?? throw new ArgumentNullException(nameof(fileReadingManager));
            this.FilePathInfo = filePathInfo ?? GetDefaultFilePathInfoValues();
        }

        public FileLoader()
        {
            this.FilePathInfo = GetDefaultFilePathInfoValues();
            this.fileReading = new FileReadingManager();
        }

        /// <inheritdoc />
        public bool LoadFile(FilePathInfo filePathInfo = null)
        {
            var path = filePathInfo ?? this.FilePathInfo;
            try
            {
                DirectoryHelper.ValidateFilePathInfo(path);
                this.Holidays = this.fileReading.ReadHolidaysFile(path);
                return this.Holidays.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc />
        public bool SaveHolidays()
        {
            //TODO
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
