using Celestial.ParvazBolourin.Core.DAL.Model.Visa;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Visa;

public sealed class AddVisaCommandReq : BaseEntity, IRequest<AddVisaCommandRes>
{
    public Visas Visa { get; set; } = new();
    public List<AddVisaLocalizedProperty> VisaLocalizedProperty { get; set; } = new();
}

public sealed class AddVisaCommandRes : ServiceResContextBase
{
}

public sealed class AddVisaLocalizedProperty
{
    public int LanguageId { get; set; }
    public string Text { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public List<Dictionary<string, string>> Embassy { get; set; } = new();
}