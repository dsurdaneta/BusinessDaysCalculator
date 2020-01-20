using System;
using System.IO;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services
{
    /// <summary>
    /// A Helper class to handle paths and directories
    /// </summary>
    public static class DirectoryHelper
    {
        public static string GenerateFilePath(FilePathInfo filePathInfo)
        {
            ValidateFilePathInfo(filePathInfo);

            var folderPath = GetFolderPath(filePathInfo);

            //if it does not exist, create directory
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return $"{folderPath}\\{filePathInfo.FileName}.{filePathInfo.Extension}";
        }

        /// <summary>
        /// Validates the file path information.
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <exception cref="ArgumentNullException">filePathInfo</exception>
        /// <exception cref="ArgumentException">The file name and file extension are needed to generate the complete file path.</exception>
        public static void ValidateFilePathInfo(FilePathInfo filePathInfo)
        {
            if (filePathInfo == null)
            {
                throw new ArgumentNullException(nameof(filePathInfo));
            }

            if (string.IsNullOrWhiteSpace(filePathInfo.FileName)
                || string.IsNullOrWhiteSpace(filePathInfo.Extension))
            {
                throw new ArgumentException("The file name and file extension are needed to generate the complete file path.");
            }
        }

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        internal static void RemoveFolder(FilePathInfo filePathInfo, bool recursive)
        {
            var folderPath = GetFolderPath(filePathInfo);

            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, recursive);
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <param name="filePathInfo">The file path information.</param>
        /// <returns></returns>
        private static string GetFolderPath(FilePathInfo filePathInfo)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folderPath = filePathInfo.IsAbsolutePath
                ? filePathInfo.Folder
                : $"{currentDirectory}\\{filePathInfo.Folder}";

            return folderPath;
        }
    }
}
