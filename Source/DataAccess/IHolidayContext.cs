using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.DataAccess
{
    /// <summary>
    /// Interface for the Database context
    /// </summary>
    public interface IHolidayContext
    {
        DbSet<DbModels.Holiday> Holidays { get; set; }

        EntityEntry<DbModels.Holiday> Entry([NotNull] DbModels.Holiday entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
