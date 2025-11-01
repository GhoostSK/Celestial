using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.FileManager;

public sealed class DeleteFileCommandReq : BaseEntity, IRequest<DeleteFileCommandRes>
{
    public List<string?> FilePath { get; set; } = new();
}

public sealed class DeleteFileCommandRes : ServiceResContextBase
{
    public bool IsDeleted { get; set; }
}