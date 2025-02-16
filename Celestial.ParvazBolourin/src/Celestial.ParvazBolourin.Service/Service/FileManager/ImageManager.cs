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
            if (!Directory.Exists(_directoryPath)) Directory.CreateDirectory(_directoryPath);
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(_directoryPath, randomFileName);
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