using Celestial.Agency.Core.Services.FileManager;
using Celestial.Agency.Service.Command.FileManager;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.FileManager;

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