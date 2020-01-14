using System;
using DsuDev.BusinessDays.DataAccess.Entites.Base;

namespace DsuDev.BusinessDays.DataAccess.Entites
{
    public class Holiday : DbEntity
    {
        public int Year { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
