using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Tools.SampleGenerators
{
    /// <summary>
    /// Class to help the creation of a FilePathInfo object
    /// </summary>
    public static class FilePathGenerator
    {
        private static readonly FilePathInfoBuilder FilePathInfoBuilder = new FilePathInfoBuilder();
        
        public static FilePathInfo CreatePath(string fileExtension)
        {
            return FilePathInfoBuilder.Create()
                .WithFolder(Resources.ContainingFolderName)
                .WithFileName(Resources.FileName)
                .WithExtension(fileExtension)
                .Build();
        }
    }
}
