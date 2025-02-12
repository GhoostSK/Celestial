using System.Linq.Expressions;
using Celestial.ParvazBolourin.Core.DAL.Model.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using Mapster;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.CommandHandler.Visa;

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
             property.LocalKeyGroup == "Visa") ||
            property.LocalKey == "Embassy_PhoneNumber";

        await _localizedPropertyService.BulkRemove(predicate, cancellationToken);
        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);
        foreach (var item in request.VisaLocalizedProperty)
        {
            var localizedData = await _localizedPropertyService.GetPropertyByLanguageAndEntityId(
                new GetLocalizedPropertyReq
                {
                    LanguageId = item.LanguageId,
                    LocalKeyGroup = "Visa",
                    EntityId = request.Id
                }, cancellationToken);

            foreach (var embassy in item.Embassy)
            foreach (var kvp in embassy)
                await _localizedPropertyService.AddAsync(new LocalizedProperty
                {
                    EntityId = request.Id,
                    LanguageId = item.LanguageId,
                    LocalKey = "Embassy_" + kvp.Key,
                    LocalValue = kvp.Value,
                    LocalKeyGroup = "Visa"
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
                }
            }
        }

        await _localizedPropertyService.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateVisaCommandRes();
    }
}