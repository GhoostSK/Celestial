using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Geo;

public class GetStateCommandReq : IRequest<GetStateCommandRes>
{
    [Required]
    public int CountryId { get; set; }
}

public class GetStateCommandRes 
{
    public List<State> Items { get; set; } = new();
}