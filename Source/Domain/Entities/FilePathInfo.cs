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

        public FilePathInfo()
        {
            Folder = string.Empty;
            FileName = string.Empty;
            Extension = string.Empty;
            IsAbsolutePath = false;
        }
    }
}
