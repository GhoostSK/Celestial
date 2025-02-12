using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class UpdateHotelCommandHandler : BaseRequestHandler<UpdateHotelCommandReq, UpdateHotelCommandRes>
{
    private readonly IHotelService _hotelService;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedPropertyService;

    public UpdateHotelCommandHandler(IHotelService hotelRepository, ILocalizedPropertyService localizedProperty,
        ILanguageService languageService)
    {
        _hotelService = hotelRepository;
        _localizedPropertyService = localizedProperty;
        _languageService = languageService;
    }

    protected override async Task<UpdateHotelCommandRes> HandleCore(UpdateHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.LocalizedProperty)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new UpdateHotelCommandRes
                {
                    Error = new ValidationError("LA400", "Invalid Language")
                };

        var hotel = await _hotelService.FindByIdAsync(cancellationToken, request.Id);
        if (hotel == null)
            return new UpdateHotelCommandRes
            {
                Error = new ValidationError("E404", "Hotel Not Found")
            };

        request.Hotel.Id = request.Id;
        request.Hotel.Adapt(hotel);
        await _hotelService.UnitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var localizedItem in request.LocalizedProperty)
        {
            var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    LanguageId = localizedItem.LanguageId,
                    LocalKeyGroup = "Hotel",
                    EntityId = request.Id
                }, cancellationToken);

            var propertiesToUpdate = new Dictionary<string, string>
            {
                { nameof(localizedItem.ShortText), localizedItem.ShortText },
                { nameof(localizedItem.MainText), localizedItem.MainText },
                { nameof(localizedItem.Address), localizedItem.Address }
            };

            foreach (var (key, value) in propertiesToUpdate)
            {
                var existingProperty = localizedData.FirstOrDefault(p => p.LocalKey == key);

                if (existingProperty == null)
                    await _localizedPropertyService.AddAsync(new LocalizedProperty
                    {
                        LanguageId = localizedItem.LanguageId,
                        LocalKeyGroup = "Hotel",
                        EntityId = request.Id,
                        LocalKey = key,
                        LocalValue = value
                    }, cancellationToken);
                else
                    existingProperty.LocalValue = value;
            }
        }

        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateHotelCommandRes();
    }
}