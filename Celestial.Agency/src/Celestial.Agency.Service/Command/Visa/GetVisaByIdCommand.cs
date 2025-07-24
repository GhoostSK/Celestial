using Celestial.Agency.Core.Enum.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.Command.Visa;

public sealed class GetVisaByIdCommandReq : BaseEntity, IRequest<GetVisaCommandRes>
{
    public string? Slug { get; set; }
}

public sealed class GetVisaCommandRes : ServiceResContextBase
{
    public string Language { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<Dictionary<string, string>> Embassy { get; set; } = new();
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = null!;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
}