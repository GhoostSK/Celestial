using MediatR;

namespace Celestial.Agency.Service.Command.Geo;

public class ValidateGeoCommandReq : IRequest<bool>
{
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public long CityId { get; set; }
}