using MediatR;
using Microsoft.AspNetCore.Http;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.FileManager;

public sealed class SaveFileCommandReq : BaseEntity, IRequest<SaveFileCommandRes>
{
    public IFormFile File { get; set; } = null!;
}

public sealed class SaveFileCommandRes : ServiceResContextBase
{
    public string FilePath { get; set; } = string.Empty;
}