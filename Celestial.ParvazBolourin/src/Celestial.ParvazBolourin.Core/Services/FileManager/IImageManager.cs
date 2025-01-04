using Microsoft.AspNetCore.Http;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.Services.FileManager;

public interface IImageManager : IScopeLifetime
{
    Task<string> SaveFileWithRandomNameAsync(IFormFile file);
    bool DeleteFile(string filePath);
    Task<byte[]> ReadFileAsBytesAsync(string filePath);
}