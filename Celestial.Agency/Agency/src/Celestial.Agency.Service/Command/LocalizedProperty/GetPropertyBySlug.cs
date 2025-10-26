using Celestial.Agency.Core.DAL.Model.Global;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

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