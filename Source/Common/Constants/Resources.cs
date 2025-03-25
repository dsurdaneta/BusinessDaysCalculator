using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DsuDev.BusinessDays.Services")]
[assembly: InternalsVisibleTo("DsuDev.BusinessDays.Services.Tests")]
[assembly: InternalsVisibleTo("DsuDev.BusinessDays.Tools")]
[assembly: InternalsVisibleTo("DsuDev.BusinessDays.Common.Tools")]
namespace DsuDev.BusinessDays.Common.Constants;


/// <summary>
/// Class with internal constants that represent where to read the resources
/// </summary>
internal static class Resources
{
    internal const string ContainingFolderName = "Resources";
    internal const string FileName = "holidays";
}
