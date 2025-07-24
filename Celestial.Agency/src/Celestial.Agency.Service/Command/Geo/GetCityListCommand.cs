using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Core.DAL.Model.Geo;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Geo;

public class GetCityListCommandReq : IRequest<GetCityListCommandRes>
{
    [Required] public int CountryId { get; set; }
}

public class GetCityListCommandRes : ServiceResContextBase
{
    public List<City> Items { get; set; } = new();
}