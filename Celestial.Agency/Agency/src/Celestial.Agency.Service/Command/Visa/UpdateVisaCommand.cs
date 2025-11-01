using Celestial.Agency.Core.DAL.Model.Visa;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Visa;

public sealed class UpdateVisaCommandReq : BaseEntity, IRequest<UpdateVisaCommandRes>
{
    public int Id { get; set; }
    public Visas Visa { get; set; } = new();
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
}

public sealed class UpdateVisaCommandRes : ServiceResContextBase
{
}