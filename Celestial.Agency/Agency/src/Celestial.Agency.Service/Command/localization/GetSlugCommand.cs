using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.localization;

public sealed class GetSlugCommandReq : BaseEntity, IRequest<GetSlugCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public LocalKeyGroupType KeyGroup { get; set; }
}

public sealed class GetSlugCommandRes : ServiceResContextBase
{
    public bool IsCorrect { get; set; } = true;
    public string Slug { get; set; } = string.Empty;
}