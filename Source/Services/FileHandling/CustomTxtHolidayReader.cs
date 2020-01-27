using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;

namespace DsuDev.BusinessDays.Services.FileHandling
{
    /// <summary>
    /// A class to read the holiday information from a customized TXT file
    /// </summary>
    public class CustomTxtHolidayReader : FileReaderBase, ICustomTxtReader
    {
        public List<Holiday> Holidays { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTxtHolidayReader"/> class.
        /// </summary>
        public CustomTxtHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            throw new System.NotImplementedException("Not yet supported, might be useful for custom rules.");
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override List<Holiday> ReadHolidaysFromFile(string absoluteFilePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
