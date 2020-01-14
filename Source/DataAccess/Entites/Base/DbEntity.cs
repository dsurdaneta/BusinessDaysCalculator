using System;

namespace DsuDev.BusinessDays.DataAccess.Entites.Base
{
    public abstract class DbEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
