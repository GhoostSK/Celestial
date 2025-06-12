using System.Linq.Expressions;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.Service.Command.ShirdalBlog.BlogCategory;
using MediatR;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Tour.TourEquipment;

public sealed class UpdateTourEquipmentCommandHandler(
    ITourEquipmentsService tourEquipmentsService,
    ILocalizedPropertyService localizedPropertyService)
    : IRequestHandler<UpdateBlogCategoryCommandReq, UpdateBlogCategoryCommandRes>
{
    public async Task<UpdateBlogCategoryCommandRes> Handle(UpdateBlogCategoryCommandReq request, CancellationToken cancellationToken)
    {
        var entity = await tourEquipmentsService.FindByIdAsync(cancellationToken, request.Id);
        if (entity == null)
        {
            return new UpdateBlogCategoryCommandRes()
            {
                Error = ValidationError.UnhandledValidationError("B404", " tourEquipment Not Found")
            };
        }
        Expression<Func<LocalizedProperty, bool>> predicate = property =>
            property.EntityId == request.Id &&
            property.LocalKey == "Name" &&
            property.LocalKeyGroup == LocalKeyGroupType.TourEquipment;

        await localizedPropertyService.BulkRemove(predicate,cancellationToken);

        var localizedNames = request.LocalizedNames.Select(name => new LocalizedProperty
        {
            EntityId = request.Id,
            LanguageId = name.LanguageId,
            LocalKey = nameof(name.Name),
            LocalKeyGroup = LocalKeyGroupType.TourEquipment,
            LocalValue = name.Name
        }).ToList();

        await localizedPropertyService.BulkInsertAsync(localizedNames, cancellationToken: cancellationToken);
        return new UpdateBlogCategoryCommandRes();
    }
}