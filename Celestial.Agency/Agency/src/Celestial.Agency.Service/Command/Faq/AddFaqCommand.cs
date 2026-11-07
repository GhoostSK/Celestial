using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Faq;

public sealed class AddFaqCommandReq : BaseEntity, IRequest<AddFaqCommandRes>
{
    public int DisplayOrder { get; set; }
    public bool Published { get; set; } = true;
    public List<FaqLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class AddFaqCommandRes : ServiceResContextBase
{
    public long Id { get; set; }
}

public sealed class FaqLocalizedPropertyItem
{
    public string Language { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}
