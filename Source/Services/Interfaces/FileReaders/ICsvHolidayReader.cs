namespace DsuDev.BusinessDays.Services.Interfaces.FileReaders
{
    /// <inheritdoc/>
    public interface ICsvHolidayReader : IHolidayFileReader
    {
        string Delimiter { get; }
        bool HasHeaderRecord { get; set; }
    }
}
