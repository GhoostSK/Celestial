using System.Linq.Expressions;
using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Core.DAL.Repositories.Global.Model;
using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.Service.Command.Visa;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;
using Mapster;

namespace Celestial.Agency.Service.CommandHandler.Visa;

public class UpdateVisaCommandHandler : BaseRequestHandler<UpdateVisaCommandReq, UpdateVisaCommandRes>
{
    private readonly ILocalizedPropertyService _localizedPropertyService;
    private readonly IVisaService _visaService;

    public UpdateVisaCommandHandler(IVisaService visaService, ILocalizedPropertyService localizedPropertyService)
    {
        _visaService = visaService;
        _localizedPropertyService = localizedPropertyService;
    }

    protected override async Task<UpdateVisaCommandRes> HandleCore(UpdateVisaCommandReq request,
        CancellationToken cancellationToken)
    {
        var visa = await _visaService.FindByIdAsync(cancellationToken, request.Id);
        if (visa == null)
            return new UpdateVisaCommandRes
            {
                Error = new ValidationError("E404", "Visa Not Found")
            };

        request.Visa.Id = request.Id;
        request.Visa.Adapt(visa);

        await _visaService.UnitOfWork.SaveChangesAsync(cancellationToken);

        Expression<Func<LocalizedProperty, bool>> predicate = property =>
            (property.EntityId == request.Id &&
             property.LocalKey == "Embassy_Address" &&
             property.LocalKeyGroup == LocalKeyGroupType.Visa) ||
            property.LocalKey == "Embassy_PhoneNumber";

        await _localizedPropertyService.BulkRemove(predicate, cancellationToken);
        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);
        foreach (var item in request.VisaLocalizedProperty)
        {
            var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    Language = item.Language,
                    LocalKeyGroup = LocalKeyGroupType.Visa,
                    EntityId = request.Id
                }, cancellationToken);

            foreach (var embassy in item.Embassy)
            foreach (var kvp in embassy)
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    EntityId = request.Id,
                    Language = item.Language,
                    LocalKey = "Embassy_" + kvp.Key,
                    LocalValue = kvp.Value,
                    LocalKeyGroup = LocalKeyGroupType.Visa
                }, cancellationToken);
            foreach (var property in localizedData)
            {
                var existingProperty = await _localizedPropertyService.FindByIdAsync(cancellationToken, property.Id);

                switch (property.LocalKey)
                {
                    case nameof(item.Text):
                        existingProperty.LocalValue = item.Text;
                        break;

                    case nameof(item.Name):
                        existingProperty.LocalValue = item.Name;
                        break;

                    case nameof(item.Slug):
                        existingProperty.LocalValue = item.Slug;
                        break;
                }
            }
        }

        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateVisaCommandRes();
    }
}