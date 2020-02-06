using Microsoft.EntityFrameworkCore;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.DataAccess
{
    /// <summary>
    /// Interface for the Database context
    /// </summary>
    public interface IHolidayContext
    {
        DbSet<DbModels.Holiday> Holidays { get; set; }
    }
}
