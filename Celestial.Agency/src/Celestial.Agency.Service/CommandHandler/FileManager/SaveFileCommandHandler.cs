using Celestial.Agency.Core.Services.FileManager;
using Celestial.Agency.Service.Command.FileManager;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.FileManager;

public sealed class SaveFileCommandHandler : BaseRequestHandler<SaveFileCommandReq, SaveFileCommandRes>
{
    private readonly IImageManager _imageManager;

    public SaveFileCommandHandler(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }

    protected override async Task<SaveFileCommandRes> HandleCore(SaveFileCommandReq request,
        CancellationToken cancellationToken)
    {
        var response = new SaveFileCommandRes();

        try
        {
            var filePath = await _imageManager.SaveFileWithRandomNameAsync(request.File);
            response.FilePath = filePath;
            if (filePath == null)
                response.Error =
                    ValidationError.UnhandledValidationError("500", "Error saving file: Invalid file format(jpg,pdf)");
        }
        catch (Exception ex)
        {
            response.Error = ValidationError.UnhandledValidationError("500", $"Error saving file: {ex.Message}");
        }

        return response;
    }
}