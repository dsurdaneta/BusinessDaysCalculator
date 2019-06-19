namespace DsuDev.BusinessDays.Domain.Entities
{
    /// <summary>
    /// Class to handle holiday file information
    /// </summary>
    public class FilePathInfo
    {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public bool IsAbsolutePath { get; set; }
    }
}
