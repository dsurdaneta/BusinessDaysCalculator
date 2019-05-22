using System;
using System.IO;
using DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services
{
    public class DirectoryHelper
    {
        public static string GenerateFilePath(FilePathInfo filePathInfo)
        {
            ValidateFilePathInfo(filePathInfo);

            string currentDirectory = Directory.GetCurrentDirectory();
            string folderPath = filePathInfo.IsAbsolutePath ?
                filePathInfo.Folder :
                $"{currentDirectory}\\{filePathInfo.Folder}";

            //if it does not exist, create directory
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return $"{folderPath}\\{filePathInfo.FileName}.{filePathInfo.Extension}";
        }

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
    }
}
