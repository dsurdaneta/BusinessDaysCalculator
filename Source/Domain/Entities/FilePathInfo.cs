namespace DsuDev.BusinessDays.Domain.Entities
{
    /// <summary>
    /// Class to handle holiday file information
    /// </summary>
    public class FilePathInfo
    {
        /// <summary>
        /// Gets or sets the folder path.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        public string Folder { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is absolute path.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is absolute path; otherwise, <c>false</c>.
        /// </value>
        public bool IsAbsolutePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePathInfo"/> class.
        /// </summary>
        public FilePathInfo()
        {
            Folder = string.Empty;
            FileName = string.Empty;
            Extension = string.Empty;
            IsAbsolutePath = false;
        }
    }
}
