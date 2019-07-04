﻿using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Tools.SampleGenerators
{
    public class FilePathGenerator
    {
        private static readonly FilePathInfoBuilder FilePathInfoBuilder = new FilePathInfoBuilder();
        public FilePathInfo CreatePath(string fileExtension)
        {
            return FilePathInfoBuilder.Create()
                .WithFolder(Resources.ContainingFolderName)
                .WithFileName(Resources.FileName)
                .WithExtension(fileExtension)
                .Build();
        }
    }
}
