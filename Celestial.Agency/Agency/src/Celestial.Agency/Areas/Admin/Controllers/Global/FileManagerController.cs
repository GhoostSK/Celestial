using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Service.Command.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Global;

public class FileManagerController : BaseController
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SaveFile([FromForm] SaveFileFormDto form)
    {
        if (form?.File == null || form.File.Length == 0)
            return BadRequest(new { Message = "No file uploaded." });

        var result = await Mediator.Send(new SaveFileCommandReq
        {
            File = form.File
        });

        if (result.HasError)
            return StatusCode(500, new { result.Error.Message });

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