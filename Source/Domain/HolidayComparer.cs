using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Domain
{
    public class HolidayComparer : IComparer<Holiday>
    {
        public enum CompareField
        {
            Name,
            Date
        }

        public CompareField SortBy = CompareField.Date;

        public int Compare(Holiday x, Holiday y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException();
            }

            var stringComparison = string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
            var comparingResult = SortBy == CompareField.Name ? stringComparison : x.HolidayDate.CompareTo(y.HolidayDate);

            return comparingResult;
        }
    }
}
