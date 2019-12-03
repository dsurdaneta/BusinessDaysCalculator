using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Tools.SampleGenerators
{
    /// <summary>
    /// Class to help the creation of a FilePathInfo object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class FilePathGenerator
    {
        private static readonly FilePathInfoBuilder FilePathInfoBuilder = new FilePathInfoBuilder();
        
        public static FilePathInfo CreateBasePath(string fileExtension)
        {
            return FilePathInfoBuilder.Create()
                .WithFolder(Resources.ContainingFolderName)
                .WithFileName(Resources.FileName)
                .WithExtension(fileExtension)
                .Build();
        }
    }
}
