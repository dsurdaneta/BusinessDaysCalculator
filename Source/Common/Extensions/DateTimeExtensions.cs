using System;

namespace DsuDev.BusinessDays.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether this DateTime instance is in a weekend.
        /// </summary>
        /// <param name="currentDateTime">The currentDateTime.</param>
        /// <returns>
        ///   <c>true</c> if the specified currentDateTime is weekend; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekend(this DateTime currentDateTime)
        {
            return currentDateTime.DayOfWeek == DayOfWeek.Saturday || currentDateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines whether this DateTime instance is a week day.
        /// </summary>
        /// <param name="currentDateTime">The currentDateTime.</param>
        /// <returns>
        ///   <c>true</c> if [is a week day] [the specified currentDateTime]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAWeekDay(this DateTime currentDateTime)
        {
            return currentDateTime.DayOfWeek > DayOfWeek.Sunday && currentDateTime.DayOfWeek < DayOfWeek.Saturday;
        }
    }
}
