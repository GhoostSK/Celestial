using Celestial.Agency.Service.Command.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Global;

public class FileManagerController : BaseController
{
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
    public async Task<IActionResult> DeleteFile([FromQuery] string filePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return BadRequest(new { Message = "File path is required." });

        var deleteFileRequest = new DeleteFileCommandReq
        {
            FilePath = new List<string> { filePath } // تبدیل به لیست
        };

        var result = await Mediator.Send(deleteFileRequest, cancellationToken);

        if (result.HasError)
            return StatusCode(500, new { result.Error.Message });

        if (!result.IsDeleted)
            return NotFound(new { Message = "File not found or could not be deleted." });

        return Ok(new { Message = "File deleted successfully." });
    }
}