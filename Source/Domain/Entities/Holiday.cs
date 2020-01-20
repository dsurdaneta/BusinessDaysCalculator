using System;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Domain.Entities
{
    /// <summary>
    /// DTO class to handle Holidays information
    /// </summary>
    public class Holiday : IEquatable<Holiday>
    {
        /// <summary>
        /// Thedefault  date format
        /// </summary>
        public const string DateFormat = "YYYY-MM-DD";

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the holiday date.
        /// </summary>
        /// <value>
        /// The holiday date.
        /// </value>
        public DateTime HolidayDate { get; set; }
        /// <summary>
        /// Gets or sets the holiday string date.
        /// </summary>
        /// <remarks>in case you need to handle the date as a string</remarks>
        /// <value>
        /// The holiday string date.
        /// </value>
        public string HolidayStringDate { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Holiday"/> class.
        /// </summary>
        /// <param name="currentYear">if set to <c>true</c> [current year].</param>
        [JsonConstructor]
        public Holiday(bool currentYear = false)
        {
            if (currentYear)
                InitializeHolidayDate(DateTime.Today.Year, 1, 1);
            else
                HolidayDate = new DateTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Holiday"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        public Holiday(int year, int month, int day) => InitializeHolidayDate(year, month, day);

        /// <summary>
        /// Initializes a new instance of the <see cref="Holiday"/> class.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public Holiday(DateTime dateTime) => InitializeHolidayDate(dateTime.Year, dateTime.Month, dateTime.Day);

        /// <summary>
        /// Initializes a new instance of the <see cref="Holiday"/> class.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public Holiday(TimeSpan timeSpan) : this (new DateTime(timeSpan.Ticks))
        {

        }

        /// <summary>
        /// Initializes the holiday date.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        private void InitializeHolidayDate(int year, int month, int day)
        {
            HolidayDate = new DateTime(year, month, day);
        }

        /// <inheritdoc />
        public bool Equals(Holiday other)
        {
            if (other == null)
                return false;

            var myHolidayUtc = HolidayDate.ToUniversalTime();
            var otherHolidayUtc = other.HolidayDate.ToUniversalTime();

            return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase)
                         && myHolidayUtc.Month == otherHolidayUtc.Month
                         && myHolidayUtc.Day == otherHolidayUtc.Day;}
    }
	
}
