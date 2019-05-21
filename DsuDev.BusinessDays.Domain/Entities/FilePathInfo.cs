namespace DsuDev.BusinessDays.Domain.Entities
{
    public class FilePathInfo
    {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public bool IsAbsolutePath { get; set; }
    }
}
