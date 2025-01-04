using Celestial.ParvazBolourin.Core.Services.FileManager;
using Microsoft.AspNetCore.Http;

namespace Celestial.ParvazBolourin.Service.Service.FileManager;

public sealed class ImageManager : IImageManager
{
    public async Task<string> SaveFileWithRandomNameAsync(IFormFile file)
    {
        const string directoryPath = "C:\\Users\\S.K\\Desktop\\Picture";
        if (file.Length > 0)
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(directoryPath, randomFileName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fullPath;
        }

        return null!;
    }

    public bool DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        return false;
    }

    public async Task<byte[]> ReadFileAsBytesAsync(string filePath)
    {
        if (File.Exists(filePath)) return await File.ReadAllBytesAsync(filePath);
        return null!;
    }
}