using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public interface IFileReadingManager
    {
        List<Holiday> ReadHolidaysFile(FilePathInfo filePathInfo);
    }
}
