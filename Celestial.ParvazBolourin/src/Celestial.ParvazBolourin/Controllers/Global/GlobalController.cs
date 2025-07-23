using Celestial.ParvazBolourin.Api.Controllers;
using Celestial.ParvazBolourin.Service.Command.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Controllers.Global;

public class GlobalController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> ReadFile([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return BadRequest(new { Message = "File path is required." });

        var readFileRequest = new ReadFileCommandReq { FilePath = filePath };
        var result = await Mediator.Send(readFileRequest);

        if (result.HasError)
            return StatusCode(500, new { result.Error.Message });

        var memoryStream = new MemoryStream(result.FileData);
        memoryStream.Position = 0;

        var contentType = GetMimeType(Path.GetExtension(filePath));

        // مهم: FileStreamResult با ContentType بدون FileDownloadName
        return new FileStreamResult(memoryStream, contentType);
    }
    private string GetMimeType(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".pdf" => "application/pdf",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };
    }
}