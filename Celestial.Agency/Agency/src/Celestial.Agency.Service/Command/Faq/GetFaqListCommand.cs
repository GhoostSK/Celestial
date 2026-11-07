using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Faq;

public sealed class GetFaqListCommandReq : BaseEntity, IRequest<GetFaqListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public bool OnlyPublished { get; set; } = true;
}

public sealed class GetFaqListCommandRes : ServiceResContextBase
{
    public List<FaqItemRes> Items { get; set; } = new();
}

public sealed class FaqItemRes
{
    public long Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool Published { get; set; }
}
