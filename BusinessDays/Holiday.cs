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
        public string HolidayStringDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
