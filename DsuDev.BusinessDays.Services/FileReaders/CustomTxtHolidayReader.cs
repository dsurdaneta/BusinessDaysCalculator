using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

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
            throw new System.NotImplementedException();
        }
    }
}
