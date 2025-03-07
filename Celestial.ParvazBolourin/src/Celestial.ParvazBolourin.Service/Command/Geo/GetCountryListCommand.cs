using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Geo;

public class GetCountryListCommandReq : IRequest<GetCountryListCommandRes>
{
}

public class GetCountryListCommandRes : ServiceResContextBase
{
    public List<Country> Items { get; set; } = new();
}