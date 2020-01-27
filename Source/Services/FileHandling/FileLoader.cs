using System;
using System.Collections.Generic;
using AutoMapper;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileHandling
{
    public class FileLoader : IFileLoader
    {
        private readonly IMapper mapper;
        private readonly IFileReadingManager fileReading;
        /// <inheritdoc />
        public DomainEntities.FilePathInfo FilePathInfo { get; set; }

        /// <inheritdoc />
        public List<DomainEntities.Holiday> Holidays { get; set; }

        public FileLoader(IMapper mapper, IFileReadingManager fileReadingManager, DomainEntities.FilePathInfo filePathInfo)
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
        public bool LoadFile(DomainEntities.FilePathInfo filePathInfo = null)
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
            var dbHolidays = this.mapper.Map<ICollection<DbModels.Holiday>>(Holidays);
            //TODO
            throw new NotImplementedException();
        }

        private static DomainEntities.FilePathInfo GetDefaultFilePathInfoValues()
        {
            return new DomainEntities.FilePathInfo
            {
                Folder = Resources.ContainingFolderName,
                FileName = Resources.FileName,
                Extension = FileExtension.Json,
                IsAbsolutePath = false
            };
        }
    }
}
