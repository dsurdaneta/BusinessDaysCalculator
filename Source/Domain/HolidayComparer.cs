using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Domain;

/// <summary>
/// Class to that compares two Holidays
/// </summary>
/// <seealso cref="System.Collections.Generic.IComparer{DsuDev.BusinessDays.Domain.Entities.Holiday}" />
public class HolidayComparer : IComparer<Holiday>
{
    /// <summary>
    /// Which field to use in the comparisson
    /// </summary>
    public enum CompareField
    {
        Name,
        Date
    }

    /// <summary>
    /// Which field must be used to sort by
    /// </summary>
    public CompareField SortBy = CompareField.Date;

    /// <summary>
    /// Comparestwo holidays (for sorting).
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public int Compare(Holiday x, Holiday y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentNullException();
        }

        var stringComparison = string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
        var comparingResult = SortBy == CompareField.Name 
                            ? stringComparison 
                            : x.HolidayDate.ToUniversalTime().CompareTo(y.HolidayDate.ToUniversalTime());

        return comparingResult;
    }
}
