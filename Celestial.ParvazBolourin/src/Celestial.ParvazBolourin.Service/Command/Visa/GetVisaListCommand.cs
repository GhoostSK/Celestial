using Celestial.ParvazBolourin.Core.Enum.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Visa;

public sealed class GetVisaListCommandReq : BaseEntity, IRequest<GetVisaListCommandRes>
{
    public int LanguageId { get; set; }
}

public sealed class GetVisaListCommandRes : ServiceResContextBase
{
    public List<GetVisaListItemRes> Items { get; set; } = new();
}

public sealed class GetVisaListItemRes
{
    public int Id { get; set; }
    public string Country { get; set; } = String.Empty;
    public bool Status { get; set; } = true;
    public String PicturePath { get; set; } = String.Empty;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public int LanguageId { get; set; }
    public string Text { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
}