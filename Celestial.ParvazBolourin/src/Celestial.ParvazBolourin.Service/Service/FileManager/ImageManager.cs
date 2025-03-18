using Celestial.ParvazBolourin.Core;
using Celestial.ParvazBolourin.Core.Services.FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Celestial.ParvazBolourin.Service.Service.FileManager;

public sealed class ImageManager : IImageManager
{
    private readonly string _directoryPath;

    public ImageManager(IOptions<AppSettings> options)
    {
        _directoryPath = options.Value.FileManager.ImageDirectory;
    }

    public async Task<string> SaveFileWithRandomNameAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            string subFolder;
            string extension = Path.GetExtension(file.FileName).ToLower();

            if (extension == ".pdf")
                subFolder = "Document";
            else if (extension == ".jpg")
                subFolder = "Image";
            else
            {
                return null!;
            }

            string directoryPath = Path.Combine(_directoryPath, subFolder);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var randomFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(directoryPath, randomFileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fullPath;
        }

        return null!;
    }

    public Dictionary<string, bool> DeleteFiles(List<string?> filePaths)
    {
        var result = new Dictionary<string, bool>();

        foreach (var filePath in filePaths)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    result[filePath] = true;  // موفقیت در حذف فایل
                }
                else
                {
                    result[filePath!] = false; // فایل وجود نداشت
                }
            }
            catch
            {
                result[filePath!] = false;     // خطا در حذف فایل
            }
        }

        return result;
    }


    public async Task<byte[]> ReadFileAsBytesAsync(string filePath)
    {
        if (File.Exists(filePath)) return await File.ReadAllBytesAsync(filePath);
        return null!;
    }
}