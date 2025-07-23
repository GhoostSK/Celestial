using Celestial.ParvazBolourin.Api.Areas.Admin.Controllers;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Service.Command.Geo;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Celestial.Service.Command.Hotel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Api.Areas.Admin.Controllers.Hotel;

public class HotelController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddHotel(HotelDto request, CancellationToken cancellationToken)
    {
        var geoValidate = await Mediator.Send(new ValidateGeoCommandReq()
        { StateId = request.StateId, CountryId = request.CountryId, CityId = request.CityId }, cancellationToken);
        if (!geoValidate)
            return BadRequest("city or country or state not valid");

        var model = new AddHotelCommandReq
        {
            Hotel = request.Adapt<Hotels>(),
            HotelLocalizeProperty = request.LanguageData,
            HotelPictures = request.Picture
        };
        var response = await Mediator.Send(model, cancellationToken);

        if (response.HasError)
            return BadRequest(response.Error);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHotelById(UpdateHotelReqDto request, CancellationToken cancellationToken)
    {
        var geoValidate = await Mediator.Send(new ValidateGeoCommandReq()
        { StateId = request.Hotel.StateId, CountryId = request.Hotel.CountryId, CityId = request.Hotel.CityId }, cancellationToken);
        if (!geoValidate)
            return BadRequest("city or country or state not valid");

        var response =
            await Mediator.Send(
                new UpdateHotelCommandReq
                { Id = request.Id, Hotel = request.Hotel, LocalizedProperty = request.LanguageData },
                cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveHotelById(int hotelId, CancellationToken cancellationToken)
    {
        var response =
            await Mediator.Send(new RemoveHotelCommandReq { HotelId = hotelId }, cancellationToken);
        if (response.HasError)
            return BadRequest(response.Error);
        return Ok(response);
    }
}