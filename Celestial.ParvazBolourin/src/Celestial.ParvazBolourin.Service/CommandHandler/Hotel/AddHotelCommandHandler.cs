using Celestial.ParvazBolourin.Core.DAL.Model.Global;
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
    private readonly IImageService _imageService;

    public AddHotelCommandHandler(IHotelService hotel, ILocalizedPropertyService localizedProperty,
        ILanguageService languageService, IImageService imageService)
    {
        _hotelRepository = hotel;
        _localizedProperty = localizedProperty;
        _languageService = languageService;
        _imageService = imageService;
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


        var hotelId = await _hotelRepository.AddHotel(data, cancellationToken);
        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.HotelPictures)
            await _imageService.AddAsync(new Images()
            {
                PicturePath = item.FilePath,
                EntityId = hotelId,
                DisplayOrder = item.DisplayOrder,
                KeyGroup = "Hotel"
            }, cancellationToken);

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
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaDescription),
                LocalValue = item.MetaDescription,
                LocalKeyGroup = "Hotel"
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaKeywords),
                LocalValue = item.MetaKeywords,
                LocalKeyGroup = "Hotel"
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaTitle),
                LocalValue = item.MetaTitle,
                LocalKeyGroup = "Hotel"
            }); 
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Slug),
                LocalValue = item.Slug,
                LocalKeyGroup = "Hotel"
            });
        }

        await _localizedProperty.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        await _imageService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new AddHotelCommandRes();
    }
}