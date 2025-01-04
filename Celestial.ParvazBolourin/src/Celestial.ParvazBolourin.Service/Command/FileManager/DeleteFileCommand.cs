using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.FileManager;

public sealed class DeleteFileCommandReq : BaseEntity, IRequest<DeleteFileCommandRes>
{
    public string FilePath { get; set; } = string.Empty;
}

public sealed class DeleteFileCommandRes : ServiceResContextBase
{
    public bool IsDeleted { get; set; }
}