using Celestial.ParvazBolourin.Core.DAL.Model.Geo;
using MediatR;

namespace Celestial.ParvazBolourin.Service.Command.Geo;

public class GetCountryCommandReq : IRequest<GetCountryCommandRes>
{
}

public class GetCountryCommandRes
{
    public List<Country> Items { get; set; } = new();
}