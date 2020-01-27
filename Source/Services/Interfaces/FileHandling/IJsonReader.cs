using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Interfaces.FileHandling
{
    /// <summary>
    /// Holiday JSON File Reader Interface
    /// </summary>
    public interface IJsonReader : IHolidayFileReader
    {
        /// <summary>
        /// Gets the holidays from the specified file asynchronously.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <returns></returns>
        Task<List<Holiday>> GetHolidaysFromFileAsync(string absoluteFilePath);
    }
}
