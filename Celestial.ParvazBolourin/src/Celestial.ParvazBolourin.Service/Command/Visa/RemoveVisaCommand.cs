using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Visa;

public sealed class RemoveVisaCommandReq : BaseEntity, IRequest<RemoveVisaCommandRs>
{
    public int Id { get; set; }
}

public sealed class RemoveVisaCommandRs : ServiceResContextBase
{

}