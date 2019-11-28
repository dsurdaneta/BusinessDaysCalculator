using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Tools.FluentBuilders
{
    /// <summary>
    /// Fluent Builder class to Build a <seea cref="FilePathInfo"/> object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FilePathInfoBuilder
    {
        private string currentFolder;
        private string fileName;
        private string ext;
        private bool isAbsolutePath;

        public FilePathInfoBuilder Create()
        {
            this.currentFolder = string.Empty;
            this.fileName = string.Empty;
            this.ext = string.Empty;
            this.isAbsolutePath = false;
            return this;
        }

        public FilePathInfoBuilder WithFileName(string name)
        {
            this.fileName = name;
            return this;
        }

        public FilePathInfoBuilder WithFolder(string folder)
        {
            this.currentFolder = folder;
            return this;
        }
        
        public FilePathInfoBuilder WithExtension(string extension)
        {
            this.ext = extension;
            return this;
        }

        public FilePathInfoBuilder WithIsAbsolutePath(bool isAbsolute)
        {
            this.isAbsolutePath = isAbsolute;
            return this;
        }

        public FilePathInfo Build()
        {
            return new FilePathInfo
            {
                Folder = this.currentFolder,
                FileName = this.fileName,
                Extension = this.ext,
                IsAbsolutePath = this.isAbsolutePath
            };
        }
    }
}
