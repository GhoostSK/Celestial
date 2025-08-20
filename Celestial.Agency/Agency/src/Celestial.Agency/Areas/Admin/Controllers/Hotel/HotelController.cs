using Celestial.Agency.Api.Models.Dto;
using Celestial.Agency.Core.DAL.Model.Hotel;
using Celestial.Agency.Service.Command.Geo;
using Celestial.Agency.Service.Command.Hotel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Hotel;

public class HotelController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddHotel([FromBody] HotelDto request, CancellationToken cancellationToken)
    {
        var geoValidate = await Mediator.Send(new ValidateGeoCommandReq
            { StateId = request.StateId, CountryId = request.CountryId, CityId = request.CityId }, cancellationToken);
        if (!geoValidate)
            return BadRequest("city or country or state not valid");

        var model = new AddHotelCommandReq
        {
            Hotel = request.Adapt<Hotels>(),
            LocalizeProperty = request.LocalizedProperty,
            HotelPictures = request.Picture
        };
        var response = await Mediator.Send(model, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHotelById([FromBody] UpdateHotelReqDto request,
        CancellationToken cancellationToken)
    {
        var geoValidate = await Mediator.Send(new ValidateGeoCommandReq
                { StateId = request.Hotel.StateId, CountryId = request.Hotel.CountryId, CityId = request.Hotel.CityId },
            cancellationToken);
        if (!geoValidate)
            return BadRequest("city or country or state not valid");

        var response =
            await Mediator.Send(
                request.Adapt<UpdateHotelCommandReq>(), cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveHotelById(RemoveHotelReqDto request, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(request.Adapt<RemoveHotelCommandReq>(), cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}