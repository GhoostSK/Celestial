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
    private readonly IImageService _imageService;

    public UpdateHotelCommandHandler(IHotelService hotelService, ILocalizedPropertyService localizedProperty,
        ILanguageService languageService, IImageService imageService)
    {
        _hotelService = hotelService;
        _localizedPropertyService = localizedProperty;
        _languageService = languageService;
        _imageService = imageService;
    }

    protected override async Task<UpdateHotelCommandRes> HandleCore(UpdateHotelCommandReq request,
        CancellationToken cancellationToken)
    {
        // 🔄 اعتبارسنجی زبان‌های ارسالی
        var languages = await _languageService.GetLanguages(cancellationToken);
        foreach (var item in request.LocalizedProperty)
        {
            if (!languages.Any(p => p.Id == item.LanguageId))
                return new UpdateHotelCommandRes
                {
                    Error = new ValidationError("LA400", "Invalid Language")
                };
        }

        // 🔍 پیدا کردن هتل در دیتابیس
        var hotel = await _hotelService.FindByIdAsync(cancellationToken, request.Id);
        if (hotel == null)
            return new UpdateHotelCommandRes
            {
                Error = new ValidationError("E404", "Hotel Not Found")
            };

        // 🏨 به‌روز رسانی اطلاعات هتل
        request.Hotel.Id = request.Id;
        request.Hotel.Adapt(hotel);
        await _hotelService.UnitOfWork.SaveChangesAsync(cancellationToken);

        // 📸 مدیریت تصاویر هتل
        var existingImages = await _imageService.GetImagesByEntityId(request.Id, "Hotel", cancellationToken);

        // حذف تصاویر قدیمی که در درخواست نیستند
        var imagesToDelete = existingImages
            .Where(dbImg => request.Images.All(reqImg => reqImg.Id != dbImg.Id))
            .ToList();

        var imageIdsToDelete = imagesToDelete.Select(img => img.Id).ToList();

        if (imageIdsToDelete.Any())
        {
            await _imageService.BulkRemove(img => imageIdsToDelete.Contains(img.Id), cancellationToken);
        }

        // به‌روز رسانی یا افزودن تصاویر جدید
        foreach (var image in request.Images)
        {
            if (image.Id > 0)
            {
                var existingImage = existingImages.FirstOrDefault(dbImg => dbImg.Id == image.Id);
                if (existingImage != null)
                {
                    existingImage.KeyGroup = image.KeyGroup;
                    existingImage.PicturePath = image.PicturePath;
                    existingImage.DisplayOrder = image.DisplayOrder;
                }
            }
            else
            {
                await _imageService.AddAsync(new Images
                {
                    KeyGroup = image.KeyGroup,
                    EntityId = request.Id,
                    PicturePath = image.PicturePath,
                    DisplayOrder = image.DisplayOrder
                }, cancellationToken);
            }
        }

        await _hotelService.UnitOfWork.SaveChangesAsync(cancellationToken);

        // 🌐 به‌روز رسانی `LocalizedProperty` ها
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
