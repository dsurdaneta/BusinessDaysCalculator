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
        public string HolidayStringDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Holiday()
        {
            HolidayDate = new DateTime();
        }

        public Holiday(int year, int month, int day)
        {
            HolidayDate = new DateTime(year, month, day);
        }

        public Holiday(DateTime dateTime)
        {
            HolidayDate = dateTime;
        }

        public Holiday(TimeSpan timeSpan)
        {
            HolidayDate = new DateTime(timeSpan.Ticks);
        }
    }

    /// <summary>
    /// DTO list class to handle a List of Holidays. 
	/// Just a Container
    /// </summary>
    public class HolidaysInfoList 
    {
        public List<Holiday> Holidays { get; set; }

        public HolidaysInfoList()
        {
            Holidays = new List<Holiday>();
        }
    }
}
