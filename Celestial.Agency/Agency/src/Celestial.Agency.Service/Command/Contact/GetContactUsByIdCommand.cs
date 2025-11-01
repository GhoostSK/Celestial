using Celestial.Agency.Core.DAL.Model.Contact;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Contact;

public sealed class GetContactUsByIdCommandReq : BaseEntity, IRequest<GetContactUsByIdCommandRes>
{
    public long Id { get; set; }
}

public sealed class GetContactUsByIdCommandRes : ServiceResContextBase
{
    public ContactUs Item { get; set; } = new();
}