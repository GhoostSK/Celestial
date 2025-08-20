using Microsoft.AspNetCore.Http;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.Services.FileManager;

public interface IImageManager : IScopeLifetime
{
    Task<string> SaveFileWithRandomNameAsync(IFormFile file);

    public Dictionary<string, bool> DeleteFiles(List<string?> filePaths);

    Task<byte[]> ReadFileAsBytesAsync(string filePath);
}