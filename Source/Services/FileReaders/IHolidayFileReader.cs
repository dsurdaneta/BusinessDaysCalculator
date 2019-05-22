using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    /// <summary>
    /// Holiday File Reader Interface
    /// </summary>
    public interface IHolidayFileReader
    {
        /// <summary>
        /// Gets or sets the holidays.
        /// </summary>
        /// <value>
        /// The holidays.
        /// </value>
        List<Holiday> Holidays { get; set; }

        /// <summary>
        /// Gets the holidays from the specified file.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        /// <returns></returns>
        List<Holiday> GetHolidaysFromFile(string absoluteFilePath);
    }
}
