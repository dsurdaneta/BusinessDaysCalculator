using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public abstract class FileReaderBase
    {
        protected abstract List<Holiday> ReadHolidaysFromFile(string absoluteFilePath);

        protected static void ValidatePath(string absoluteFilePath, string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentNullException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{fileExtension}"))
            {
                throw new InvalidOperationException($"File extension {fileExtension} was expected");
            }
        }
    }
}
