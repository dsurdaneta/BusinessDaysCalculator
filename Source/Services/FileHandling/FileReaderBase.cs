using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileHandling
{
    public abstract class FileReaderBase
    {
        /// <summary>
        /// Reads the holidays from file.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <returns></returns>
        protected abstract List<Holiday> ReadHolidaysFromFile(string absoluteFilePath);

        /// <summary>
        /// Validates the file path.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <exception cref="ArgumentNullException">absoluteFilePath</exception>
        /// <exception cref="InvalidOperationException">File extension {fileExtension} was expected</exception>
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
