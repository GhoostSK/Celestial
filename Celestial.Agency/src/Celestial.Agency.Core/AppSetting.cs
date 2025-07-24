namespace Celestial.Agency.Core;

public class AppSettings
{
    public FileManagerSettings FileManager { get; set; } = new();
}

public class FileManagerSettings
{
    public string ImageDirectory { get; set; } = string.Empty;
}