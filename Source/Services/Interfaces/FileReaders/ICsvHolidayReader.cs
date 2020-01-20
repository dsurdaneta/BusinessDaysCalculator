namespace DsuDev.BusinessDays.Services.Interfaces.FileReaders
{
    /// <summary>
    /// Holiday CSV File Reader Interface
    /// </summary>
    public interface ICsvHolidayReader : IHolidayFileReader
    {
        /// <summary>
        /// Gets the delimiter.
        /// </summary>
        /// <value>
        /// The delimiter.
        /// </value>
        string Delimiter { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has header record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has header record; otherwise, <c>false</c>.
        /// </value>
        bool HasHeaderRecord { get; set; }
    }
}
