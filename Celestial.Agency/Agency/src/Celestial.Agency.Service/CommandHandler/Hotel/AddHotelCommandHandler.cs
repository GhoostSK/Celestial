using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Hotel;
using Celestial.Agency.Service.Command.Hotel;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;
using MediatR;

namespace Celestial.Agency.Service.CommandHandler.Hotel;

public sealed class AddHotelCommandHandler(
    IHotelService hotel,
    ILocalizedPropertyService localizedProperty,
    IImageService imageService,
    IMediator mediator)
    : BaseRequestHandler<AddHotelCommandReq, AddHotelCommandRes>
{
    protected override async Task<AddHotelCommandRes> HandleCore(AddHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string>();
        foreach (var lang in request.LocalizeProperty)
        {
            lang.Language = lang.Language?.ToLowerInvariant()!;
            langs.Add(lang.Language);
        }

        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new AddHotelCommandRes
            {
                Error = validateLanguage.Error
            };
        foreach (var localizeSlug in request.LocalizeProperty)
        {
            var checkSlug = await localizedProperty.FirstOrDefaultAsync(
                p => p.LocalKeyGroup == LocalKeyGroupType.Hotel && p.LocalKey == "Slug" &&
                     p.LocalValue == localizeSlug.Slug && p.Language == localizeSlug.Language,
                cancellationToken: cancellationToken);
            if (checkSlug != null)
                return new AddHotelCommandRes
                {
                    Error = ValidationError.UnhandledValidationError("S403",
                        $"Slug Already exist({checkSlug.Language})")
                };
        }

        var data = request.Hotel;

        var hotelId = await hotel.AddHotel(data, cancellationToken);
        var localizedProperties = new List<LocalizedProperty>();

        foreach (var item in request.HotelPictures)
            await imageService.AddAsync(new Images
            {
                PicturePath = item.FilePath,
                EntityId = hotelId,
                DisplayOrder = item.DisplayOrder,
                KeyGroup = "Hotel"
            }, cancellationToken);

        foreach (var item in request.LocalizeProperty)
        {
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.ShortText),
                LocalValue = item.ShortText,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.MainText),
                LocalValue = item.MainText,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.Address),
                LocalValue = item.Address,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.MetaDescription),
                LocalValue = item.MetaDescription,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.MetaKeywords),
                LocalValue = item.MetaKeywords,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
                LocalKey = nameof(item.MetaTitle),
                LocalValue = item.MetaTitle,
                LocalKeyGroup = LocalKeyGroupType.Hotel
            });
            localizedProperties.Add(new LocalizedProperty
            {
                EntityId = hotelId,
                Language = item.Language,
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