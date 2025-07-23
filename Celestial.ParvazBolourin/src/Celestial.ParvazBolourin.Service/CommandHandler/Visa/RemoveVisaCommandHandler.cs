using Celestial.ParvazBolourin.Core.DAL.Repositories.Visa;
using Celestial.ParvazBolourin.Service.Command.Visa;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.Visa;

public sealed class RemoveVisaCommandHandler(IVisaService visaRepository)
    : BaseRequestHandler<RemoveVisaCommandReq, RemoveVisaCommandRs>
{
    protected override async Task<RemoveVisaCommandRs> HandleCore(RemoveVisaCommandReq request,
        CancellationToken cancellationToken)
    {
        var visa = await visaRepository.GetVisaById(request.Id, cancellationToken);
        if (visa == null)
            return new RemoveVisaCommandRs
            {
                Error = new ValidationError("404", "Visa Not Found")
            };

        visa.IsDeleted = true;

        await visaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveVisaCommandRs();
    }
}