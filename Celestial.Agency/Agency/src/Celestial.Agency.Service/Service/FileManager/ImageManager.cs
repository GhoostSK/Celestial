using Celestial.Agency.Core;
using Celestial.Agency.Core.Services.FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Celestial.Agency.Service.Service.FileManager;

public sealed class ImageManager : IImageManager
{
    private readonly string _directoryPath;

    public ImageManager(IOptions<AppSettings> options, IHostEnvironment environment)
    {
        var configuredPath = options.Value.FileManager.ImageDirectory;

        // مسیر نسبی نسبت به ریشه‌ی پروژه حل می‌شود؛ در غیر این صورت مسیری مثل
        // «/wwwroot/Images» به ریشه‌ی درایو اشاره می‌کرد و هیچ فایلی پیدا نمی‌شد.
        _directoryPath = Path.IsPathFullyQualified(configuredPath)
            ? configuredPath
            : Path.Combine(environment.ContentRootPath, configuredPath.TrimStart('/', '\\'));
    }

    public async Task<string> SaveFileWithRandomNameAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            string subFolder;
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension == ".pdf")
                subFolder = "Document";
            else if (extension == ".jpg")
                subFolder = "Image";
            else
                return null!;

            var directoryPath = Path.Combine(_directoryPath, subFolder);
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
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    result[filePath] = true;
                }
                else
                {
                    result[filePath!] = false;
                }
            }
            catch
            {
                result[filePath!] = false;
            }

        return result;
    }

    public async Task<byte[]> ReadFileAsBytesAsync(string filePath)
    {
        var fullPath = ResolvePath(filePath);

        if (File.Exists(fullPath))
            return await File.ReadAllBytesAsync(fullPath);

        return null!;
    }

    /// <summary>
    ///     مسیر ذخیره‌شده در دیتابیس را به مسیر فیزیکی تبدیل می‌کند و اجازه نمی‌دهد
    ///     با «..» از پوشه‌ی تصاویر بیرون رفت.
    /// </summary>
    private string ResolvePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return string.Empty;

        var relativePath = filePath.TrimStart('/', '\\');
        var fullPath = Path.GetFullPath(Path.Combine(_directoryPath, relativePath));
        var rootPath = Path.GetFullPath(_directoryPath);

        return fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase) ? fullPath : string.Empty;
    }
}