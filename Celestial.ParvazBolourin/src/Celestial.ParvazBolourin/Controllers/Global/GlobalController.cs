using Celestial.ParvazBolourin.Service.Command.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Global;

public class GlobalController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> ReadFile([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return BadRequest(new { Message = "File path is required." });
        var readFileRequest = new ReadFileCommandReq { FilePath = filePath };
        var result = await Mediator.Send(readFileRequest);
        if (result.HasError) return StatusCode(500, new { result.Error.Message });
        var memoryStream = new MemoryStream(result.FileData);
        memoryStream.Position = 0;

        return new FileStreamResult(memoryStream, "application/octet-stream")
        {
            FileDownloadName = Path.GetFileName(filePath)
        };
    }

    [HttpPost]
    public async Task<IActionResult> SaveFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest(new { Message = "No file uploaded." });

        var saveFileRequest = new SaveFileCommandReq
        {
            File = file
        };
        var result = await Mediator.Send(saveFileRequest);
        if (result.HasError) return StatusCode(500, new { result.Error.Message });

        return Ok(new { result.FilePath });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFile([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return BadRequest(new { Message = "File path is required." });
        var deleteFileRequest = new DeleteFileCommandReq
        {
            FilePath = filePath
        };

        var result = await Mediator.Send(deleteFileRequest);

        if (result.HasError) return StatusCode(500, new { result.Error.Message });

        return Ok(new { Message = "File deleted successfully.", result.IsDeleted });
    }
}