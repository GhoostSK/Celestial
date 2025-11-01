using Celestial.Agency.Core.DAL.Repositories.Visa;
using Celestial.Agency.Service.Command.Visa;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.Visa;

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