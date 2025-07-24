using Celestial.Agency.Core.DAL.Model.Geo;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Geo;

public class GetCountryListCommandReq : IRequest<GetCountryListCommandRes>
{
}

public class GetCountryListCommandRes : ServiceResContextBase
{
    public List<Country> Items { get; set; } = new();
}