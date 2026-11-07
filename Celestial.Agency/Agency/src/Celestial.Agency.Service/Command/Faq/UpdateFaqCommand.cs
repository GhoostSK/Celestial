using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Faq;

public sealed class UpdateFaqCommandReq : BaseEntity, IRequest<UpdateFaqCommandRes>
{
    public long Id { get; set; }
    public int DisplayOrder { get; set; }
    public bool Published { get; set; } = true;
    public List<FaqLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class UpdateFaqCommandRes : ServiceResContextBase
{
}
