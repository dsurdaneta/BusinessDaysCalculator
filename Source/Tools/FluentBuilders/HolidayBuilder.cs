using System;
using System.Globalization;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Tools.FluentBuilders
{
    public class HolidayBuilder
    {
        private DateTime date;
        private string name;
        private string description;
        //in case its needed
        private string dateString;

        public HolidayBuilder Create()
        {
            this.date = new DateTime();
            this.dateString = string.Empty;
            this.description = string.Empty;
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
            this.description = description;
            return this;
        }

        public HolidayBuilder WithDate(DateTime dateTime)
        {
            this.date = dateTime;
            //in case its needed
            this.dateString = this.date.ToString(Holiday.DateFormat, CultureInfo.InvariantCulture);
            return this;
        }

        public Holiday Build()
        {
            return new Holiday
            {
                Name = this.name,
                HolidayDate = this.date,
                HolidayStringDate = this.dateString,
                Description = this.description
            };
        }
    }
}
