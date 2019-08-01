using System;
using System.Collections.Generic;
using System.Text;

namespace DsuDev.BusinessDays.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsAWeekDay(this DateTime dt)
        {
            return dt.DayOfWeek > DayOfWeek.Sunday && dt.DayOfWeek < DayOfWeek.Saturday;
        }
    }
}
