using System;
using System.ComponentModel.DataAnnotations;
using DsuDev.BusinessDays.DataAccess.Entites.Base;

namespace DsuDev.BusinessDays.DataAccess.Entites
{
    public class Holiday : DbEntity
    {
        public int Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime HolidayDate { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string Description { get; set; }
    }
}
