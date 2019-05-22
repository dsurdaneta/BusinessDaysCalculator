namespace DsuDev.BusinessDays.Services.FileReaders
{
    public interface ICsvReader : IHolidayFileReader
    {
        string Delimiter { get; }
        bool HasHeaderRecord { get; set; }
    }
}
