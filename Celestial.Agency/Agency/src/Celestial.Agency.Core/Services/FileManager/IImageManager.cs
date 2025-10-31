using Celestial.Dal.Ef.SqlServer.Extensions;
using Microsoft.AspNetCore.Http;

namespace Celestial.Agency.Core.Services.FileManager;

public interface IImageManager : IScopeLifetime
{
    Task<string> SaveFileWithRandomNameAsync(IFormFile file);

    public Dictionary<string, bool> DeleteFiles(List<string?> filePaths);

    Task<byte[]> ReadFileAsBytesAsync(string filePath);
}