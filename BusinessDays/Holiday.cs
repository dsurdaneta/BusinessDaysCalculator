using System;
using System.Collections.Generic;

namespace DsuDev.BusinessDays
{
    /// <summary>
    /// DTO class to handle Holidays info
    /// </summary>
    public class Holiday
    {
        public DateTime HolidayDate { get; set; }
        //in case you need to handle the date as a string:
        //public string HolidayStringDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// DTO list class to handle a List of Holidays
    /// </summary>
    public class HolidaysInfoList //: List<Holiday>
    {
        public List<Holiday> Holidays { get; set; }
    }
}
