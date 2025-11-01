using Celestial.Agency.Core.DAL.Model.Geo;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Geo;

public class GetCountryListCommandReq : IRequest<GetCountryListCommandRes>
{
}

public class GetCountryListCommandRes : ServiceResContextBase
{
    public List<Country> Items { get; set; } = new();
}