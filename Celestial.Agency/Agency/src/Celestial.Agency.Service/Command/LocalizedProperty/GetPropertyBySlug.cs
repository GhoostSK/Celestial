using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.LocalizedProperty;

public sealed class GetEntityIdBySlugCommandReq : BaseEntity, IRequest<GetEntityIdBySlugCommandRes>
{
    public required string Slug { get; init; }
    public required LocalKeyGroupType GroupType { get; init; }
}

public sealed class GetEntityIdBySlugCommandRes : ServiceResContextBase
{
    public int EntityId { get; set; }
}