using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Hotel;
using Celestial.ParvazBolourin.Service.Command.Hotel;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Hotel;

public sealed class AddHotelCommandHandler(
    IHotelService hotel,
    ILocalizedPropertyService localizedProperty,
    ILanguageService languageService,
    IImageService imageService)
    : BaseRequestHandler<AddHotelCommandReq, AddHotelCommandRes>
{
    protected override async Task<AddHotelCommandRes> HandleCore(AddHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        foreach (var item in request.HotelLocalizeProperty)
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new AddHotelCommandRes
                {
                    Error = new ValidationError("LA400", "invalid Language")
                };
        foreach (var localizeSlug in request.HotelLocalizeProperty)
        {
            var checkSlug = await localizedProperty.FirstOrDefaultAsync(p => p.LocalKeyGroup == LocalKeyGroupType.Hotel && p.LocalKey == "Slug" && p.LocalValue == localizeSlug.Slug && p.LanguageId == localizeSlug.LanguageId, cancellationToken: cancellationToken);
            if (checkSlug != null)
            {
                return new AddHotelCommandRes()
                {
                    Error = ValidationError.UnhandledValidationError("S403", $"Slug Already exist({checkSlug.LanguageId})")
                };
            }
        }

        var data = request.Hotel;


        var hotelId = await hotel.AddHotel(data, cancellationToken);
        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.HotelPictures)
            await imageService.AddAsync(new Images()
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
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MainText),
                LocalValue = item.MainText,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Address),
                LocalValue = item.Address,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaDescription),
                LocalValue = item.MetaDescription,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaKeywords),
                LocalValue = item.MetaKeywords,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.MetaTitle),
                LocalValue = item.MetaTitle,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                LanguageId = item.LanguageId,
                LocalKey = nameof(item.Slug),
                LocalValue = item.Slug,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
        }

        await localizedProperty.BulkInsertAsync(localizedProperties, cancellationToken: cancellationToken);
        await imageService.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new AddHotelCommandRes();
    }
}