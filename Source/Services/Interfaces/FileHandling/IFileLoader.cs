using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Interfaces.FileHandling
{
    public interface IFileLoader
    {
        FilePathInfo FilePathInfo { get; set; }
        List<Holiday> Holidays { get; set; }

        bool LoadFile(FilePathInfo filePathInfo = null);
        bool SaveHolidays();
    }
}
