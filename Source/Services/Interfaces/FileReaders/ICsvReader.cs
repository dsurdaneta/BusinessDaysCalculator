namespace DsuDev.BusinessDays.Services.Interfaces.FileReaders
{
    public interface ICsvReader : IHolidayFileReader
    {
        string Delimiter { get; }
        bool HasHeaderRecord { get; set; }
    }
}
