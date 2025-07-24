using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.FileManager;

public sealed class ReadFileCommandReq : BaseEntity, IRequest<ReadFileCommandRes>
{
    public string FilePath { get; set; } = string.Empty;
}

public sealed class ReadFileCommandRes : ServiceResContextBase
{
    public byte[] FileData { get; set; } = null!;
}