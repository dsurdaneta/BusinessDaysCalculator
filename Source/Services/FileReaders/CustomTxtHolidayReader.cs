using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public class CustomTxtHolidayReader : ICustomTxtReader
    {
        public List<Holiday> Holidays { get; set; }

        public CustomTxtHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            throw new System.NotImplementedException("Not yet supported, might be useful for custom rules.");
        }
    }
}
