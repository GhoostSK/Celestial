using Celestial.ParvazBolourin.Core.Enum.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Visa;
public sealed class GetVisaByIdCommandReq : BaseEntity, IRequest<GetVisaByIdCommandRes>
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
}

public sealed class GetVisaByIdCommandRes : ServiceResContextBase
{
    public int LanguageId { get; set; }
    public string Text { get; set; } = String.Empty;

    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public List<Dictionary<string, string>> Embassy { get; set; } = new();
    public bool Status { get; set; } = true;
    public String PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
}

