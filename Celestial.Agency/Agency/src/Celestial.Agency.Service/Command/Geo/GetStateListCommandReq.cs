using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.DAL.Model.Geo;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Geo;

public class GetStateListCommandReq : IRequest<GetStateListCommandRes>
{
    [Required] public int CountryId { get; set; }
}

public class GetStateListCommandRes : ServiceResContextBase
{
    public List<State> Items { get; set; } = new();
}