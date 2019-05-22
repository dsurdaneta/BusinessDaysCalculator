using System;

namespace DsuDev.BusinessDays.Tools.FluentBuilders
{
    public class DateBuilder
    {
        private int day;
        private int month;
        private int year;

        public DateBuilder CreateDate()
        {
            this.day = 0;
            this.month = 0;
            this.year = 0;
            return this;
        }

        public DateBuilder WithYear(int year)
        {
            this.year = day;
            return this;
        }

        public DateBuilder WithMonth(int month)
        {
            this.month = month;
            return this;
        }

        public DateBuilder WithDay(int day)
        {
            this.day = day;
            return this;
        }

        public DateTime Build()
        {
            return new DateTime(this.year, this.month, this.day);
        }
    }
}
