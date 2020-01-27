using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Interfaces
{
    /// <summary>
    /// Interface to handle every calculation related with business days and/or holidays.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculates the number of business days between two given dates. Reads the Holidays from a provider.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The amount of business days (double) between the given dates.</returns>
        double CountBusinessDays(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Calculates the number of business days between two given dates, considering also the given holiday collection.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="holidays">The holidays.</param>
        /// <returns>The amount of business days (double) between the given dates.</returns>
        double CountBusinessDays(DateTime startDate, DateTime endDate, ICollection<Holiday> holidays);

        /// <summary>
        /// Adds a specific number of business days. Reads the Holidays from a provider.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="daysCount">The days count.</param>
        /// <returns>A new DateTime with the calculated date.</returns>
        Task<DateTime> AddBusinessDaysAsync(DateTime startDate, double daysCount);

        /// <summary>
        /// Adds a specific number of business days, considering also a number of holidays.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="daysCount">The days count.</param>
        /// <param name="notWeekendHolidaysCount">The not weekend holidays count.</param>
        /// <returns>A new DateTime with the calculated date.</returns>
        DateTime AddBusinessDays(DateTime startDate, double daysCount, double notWeekendHolidaysCount);

        /// <summary>
        ///  Adds a specific number of business days, considering also the holiday given holiday collection.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="daysCount">The days count.</param>
        /// <param name="holidays">The holidays.</param>
        /// <returns>A new DateTime with the calculated date.</returns>
        DateTime AddBusinessDays(DateTime startDate, double daysCount, ICollection<Holiday> holidays);
    }
}
