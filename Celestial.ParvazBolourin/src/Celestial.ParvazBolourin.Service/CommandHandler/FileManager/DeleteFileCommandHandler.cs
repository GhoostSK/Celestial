using Celestial.ParvazBolourin.Core.Services.FileManager;
using Celestial.ParvazBolourin.Service.Command.FileManager;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.FileManager;

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
            var deleteResults = _imageManager.DeleteFiles(request.FilePath);

            if (deleteResults.Values.All(result => result))
            {
                response.IsDeleted = true;
            }
            else
            {
                response.IsDeleted = false;
                var failedFiles = deleteResults.Where(x => !x.Value).Select(x => x.Key).ToList();
                response.Error = ValidationError.UnhandledValidationError("500",
                    $"Error deleting files: {string.Join(", ", failedFiles)} not found or failed to delete");
            }
        }
        catch (Exception ex)
        {
            response.IsDeleted = false;
            response.Error = ValidationError.UnhandledValidationError("500", $"Error deleting files: {ex.Message}");
        }

        return Task.FromResult(response);
    }
}