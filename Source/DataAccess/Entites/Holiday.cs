using System;

namespace DsuDev.BusinessDays.DataAccess.Entites
{
    public class Holiday
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
