using System;
using System.ComponentModel.DataAnnotations;

namespace DsuDev.BusinessDays.DataAccess.Entites.Base
{
    public abstract class DbEntity
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }
    }
}
