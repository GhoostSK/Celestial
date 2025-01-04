using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Model.Hotel;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class AddHotelCommandHandler : BaseRequestHandler<AddHotelCommandReq, AddHotelCommandRes>
{
    private readonly IHotelService _hotelRepository;
    private readonly ILanguageService _languageService;
    private readonly ILocalizedPropertyService _localizedProperty;

    public AddHotelCommandHandler(IHotelService hotel, ILocalizedPropertyService localizedProperty,
        ILanguageService languageService)
    {
        _hotelRepository = hotel;
        _localizedProperty = localizedProperty;
        _languageService = languageService;
    }

    protected override async Task<AddHotelCommandRes> HandleCore(AddHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.HotelLocalizeProperty)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new AddHotelCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };

        var data = request.Hotel;

        foreach (var item in request.HotelPicture)
            data.HotelPictures.Add(new HotelPicture
            {
                DisplayOrder = item.DisplayOrder,
                VirtualPath = item.FilePath
            });

        var hotelId = await _hotelRepository.AddHotel(data, cancellationToken);
        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.HotelLocalizeProperty)
        {
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.ShortText),
                LocalValue = item.ShortText,
                LocalKeyGroup = "Hotel"
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MainText),
                LocalValue = item.MainText,
                LocalKeyGroup = "Hotel"
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Address),
                LocalValue = item.Address,
                LocalKeyGroup = "Hotel"
            });
        }

        await _localizedProperty.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        return new AddHotelCommandRes();
    }
}