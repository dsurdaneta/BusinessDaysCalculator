using System.Diagnostics.CodeAnalysis;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Common.Tools.FluentBuilders;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Common.Tools.SampleGenerators
{
    /// <summary>
    /// Class to help the sample creation of a FilePathInfo object
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
