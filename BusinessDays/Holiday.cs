using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDays
{
    /// <summary>
    /// DTO class to handle Holidays info
    /// </summary>
    public class Holiday
    {
        public DateTime HolidayDate { get; set; }
        //public string HolidayStringDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// DTO list class
    /// </summary>
    public class HolidaysInfoList //: List<Holiday>
    {
        public List<Holiday> Holidays { get; set; }
    }
}
