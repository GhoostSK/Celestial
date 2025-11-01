using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Geo;

public class GetStateListCommandReq : IRequest<GetStateListCommandRes>
{
    [Required] public int CountryId { get; set; }
}

public class GetStateListCommandRes : ServiceResContextBase
{
    public List<State> Items { get; set; } = new();
}