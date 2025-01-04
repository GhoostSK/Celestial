using Celestial.ParvazBolourin.Core.Services.FileManager;
using Celestial.ParvazBolourin.Service.Command.FileManager;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.FileManager;

public sealed class DeleteFileCommandHandler : BaseRequestHandler<DeleteFileCommandReq, DeleteFileCommandRes>
{
    private readonly IImageManager _imageManager;

    public DeleteFileCommandHandler(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }

    protected override Task<DeleteFileCommandRes> HandleCore(DeleteFileCommandReq request,
        CancellationToken cancellationToken)
    {
        var response = new DeleteFileCommandRes();

        try
        {
            var isDeleteFile = _imageManager.DeleteFile(request.FilePath);
            if (!isDeleteFile)
            {
                response.IsDeleted = false;
                response.Error = ValidationError.UnhandledValidationError("500", "Error deleting file: file Not Found");
                return Task.FromResult(response);
            }

            response.IsDeleted = true;
        }
        catch (Exception ex)
        {
            response.IsDeleted = false;
            response.Error = ValidationError.UnhandledValidationError("500", $"Error deleting file: {ex.Message}");
        }

        return Task.FromResult(response);
    }
}