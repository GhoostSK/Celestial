using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Visa;

public sealed class UpdateVisaCommandReq : BaseEntity, IRequest<UpdateVisaCommandRes>
{
    public int Id { get; set; }
    public Visas Visa { get; set; } = new();
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
}

public sealed class UpdateVisaCommandRes : ServiceResContextBase
{
}