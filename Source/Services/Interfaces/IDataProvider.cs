using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Interfaces
{
    public interface IDataProvider
    {
        Task<ICollection<Holiday>> GetAllHolidaysAsync();

        ICollection<Holiday> GetHolidays(int year);

        ICollection<Holiday> GetHolidays(DateTime startDateTime, DateTime endDateTime);
    }
}
