using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Celestial.Agency.Service.Command.FileManager;

public sealed class SaveFileCommandReq : BaseEntity, IRequest<SaveFileCommandRes>
{
    public IFormFile File { get; set; } = null!;
}

public sealed class SaveFileCommandRes : ServiceResContextBase
{
    public string FilePath { get; set; } = string.Empty;
}