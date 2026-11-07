using Celestial.Agency.Core.Enum.Visa;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Visa;

public sealed class GetVisaListCommandReq : BaseEntity, IRequest<GetVisaListCommandRes>
{
    public string Language { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public sealed class GetVisaListCommandRes : ServiceResContextBase
{
    public List<GetVisaListItemRes> Items { get; set; } = new();
}

public sealed class GetVisaListItemRes
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string PicturePath { get; set; } = string.Empty;
    public VisaIssuer VisaIssuer { get; set; }
    public int IssueDate { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    //seo
    public string Slug { get; set; } = string.Empty;
}