using System;
using System.Collections.Generic;
using System.Text;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Interfaces
{
    public interface ICalculator
    {
        double GetBusinessDaysCount(DateTime startDate, DateTime endDate);
        double GetBusinessDaysCount(DateTime startDate, DateTime endDate, ICollection<Holiday> holidays);
        DateTime AddBusinessDays(DateTime startDate, double daysCount);
        DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount);
        DateTime AddBusinessDays(DateTime startDate, double daysCount, ICollection<Holiday> holidays);
    }
}
