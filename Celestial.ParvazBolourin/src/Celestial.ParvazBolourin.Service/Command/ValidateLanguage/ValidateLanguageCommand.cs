using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ValidateLanguage;

public class ValidateLanguageCommandReq : BaseEntity, IRequest<ValidateLanguageCommandRes>
{
    public List<string> Language { get; set; } = new();
}

public class ValidateLanguageCommandRes : ServiceResContextBase
{
}