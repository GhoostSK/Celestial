using Celestial.ParvazBolourin.Core.Services.FileManager;
using Celestial.ParvazBolourin.Service.Command.FileManager;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.FileManager;

public sealed class ReadFileCommandHandler : BaseRequestHandler<ReadFileCommandReq, ReadFileCommandRes>
{
    private readonly IImageManager _imageManager;

    public ReadFileCommandHandler(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }

    protected override async Task<ReadFileCommandRes> HandleCore(ReadFileCommandReq request,
        CancellationToken cancellationToken)
    {
        var response = new ReadFileCommandRes();

        try
        {
            var fileData = await _imageManager.ReadFileAsBytesAsync(request.FilePath);
            if (fileData != null)
            {
                response.FileData = fileData;
                return response;
            }

            response.Error = ValidationError.UnhandledValidationError("500", "Error reading file: File not found");
        }
        catch (Exception ex)
        {
            response.Error = ValidationError.UnhandledValidationError("500", $"Error reading file: {ex.Message}");
        }

        return response;
    }
}