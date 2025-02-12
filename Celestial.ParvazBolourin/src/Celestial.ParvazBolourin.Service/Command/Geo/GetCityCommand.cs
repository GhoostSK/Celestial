using System.ComponentModel.DataAnnotations;
using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Geo;

public class GetCityCommandReq : IRequest<GetCityCommandRes>
{
    [Required] public int StateId { get; set; }
}

public class GetCityCommandRes
{
    public List<City> Items { get; set; } = new();
}