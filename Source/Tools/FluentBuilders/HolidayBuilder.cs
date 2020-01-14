using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Common.Tools.FluentBuilders
{
    /// <summary>
    /// Fluent Builder class to Build a <seea cref="Holiday"/> object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HolidayBuilder
    {
        private DateTime date;
        private string name;
        private string desc;

        public HolidayBuilder Create()
        {
            this.date = new DateTime();
            this.desc = string.Empty;
            this.name = string.Empty;
            return this;
        }

        public HolidayBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public HolidayBuilder WithDescription(string description)
        {
            this.desc = description;
            return this;
        }

        public HolidayBuilder WithDate(DateTime dateTime)
        {
            this.date = dateTime;
            return this;
        }
        
        public HolidayBuilder WithDate(int year, int month, int day)
        {
            this.date = new DateTime(year, month, day);
            return this;
        }

        public Holiday Build()
        {
            return new Holiday
            {
                Name = this.name,
                HolidayDate = this.date,
                HolidayStringDate = this.date.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture),
                Description = this.desc
            };
        }
    }
}
