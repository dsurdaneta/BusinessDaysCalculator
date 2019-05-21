using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public interface IHolidayFileReader
    {
        List<Holiday> Holidays { get; set; }

        List<Holiday> HolidaysFromFile(string absoluteFilePath);
    }
}
