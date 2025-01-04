using Celestial.ParvazBolourin.Core.DAL.Model.Contact;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Contact;

public sealed class GetContactUsByIdCommandReq : BaseEntity, IRequest<GetContactUsByIdCommandRes>
{
    public long Id { get; set; }
}

public sealed class GetContactUsByIdCommandRes : ServiceResContextBase
{
    public ContactUs Item { get; set; } = new();
}