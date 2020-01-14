using System;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Domain.Entities
{
    /// <summary>
    /// DTO class to handle Holidays information
    /// </summary>
    public class Holiday : IEquatable<Holiday>
    {
        public const string DateFormat = "YYYY-MM-DD";
        
        internal string Id { get; set; }
        public DateTime HolidayDate { get; set; }
        //in case you need to handle the date as a string:
        public string HolidayStringDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public Holiday(bool currentYear = false)
        {
            if (currentYear)
                InitializeHolidayDate(DateTime.Today.Year, 1, 1);
            else
                HolidayDate = new DateTime();
        }

        public Holiday(int year, int month, int day) => InitializeHolidayDate(year, month, day);

        public Holiday(DateTime dateTime) => HolidayDate = dateTime;

        public Holiday(TimeSpan timeSpan) : this (new DateTime(timeSpan.Ticks))
        {

        }

        private void InitializeHolidayDate(int year, int month, int day)
        {
            HolidayDate = new DateTime(year, month, day);
        }

        public bool Equals(Holiday other)
        {
            return other != null
                    && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase) 
                    && HolidayDate.Month == other.HolidayDate.Month 
                    && HolidayDate.Day == other.HolidayDate.Day;
        }
    }
	
}
