namespace DsuDev.BusinessDays.Services.Interfaces.FileReaders
{
    public interface ICsvHolidayReader : IHolidayFileReader
    {
        string Delimiter { get; }
        bool HasHeaderRecord { get; set; }
    }
}
