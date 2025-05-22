using System;

namespace ASPCoreWebApp.clsCommon
{
    public class FileHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string rootPath, string subFolder = "UploadedFiles", string? uniqueId = null, string? SubId = null)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string baseFolder = Path.Combine(rootPath, subFolder);

            // Build the full nested folder path
            string uniqueFolder = !string.IsNullOrWhiteSpace(uniqueId)? Path.Combine(baseFolder, uniqueId): baseFolder;

            string finalFolderPath = !string.IsNullOrWhiteSpace(SubId)? Path.Combine(uniqueFolder, SubId): uniqueFolder;

            Directory.CreateDirectory(finalFolderPath); // Ensures entire path exists

            // Use original file name (or sanitize it if needed)
            string safeFileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(finalFolderPath, safeFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for saving in DB etc.
            string relativePath = Path.Combine(subFolder, uniqueId ?? "", SubId ?? "", safeFileName).Replace("\\", "/");

            return relativePath;
        }
    }
}
