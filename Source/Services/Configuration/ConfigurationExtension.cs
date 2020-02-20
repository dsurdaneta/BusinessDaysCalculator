using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DsuDev.BusinessDays.Services.Configuration
{
    public static class ConfigurationExtension
    {

        /// <summary>
        /// Determines whether this configuration instance is loaded.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if the specified configuration is loaded; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }
    }
}
