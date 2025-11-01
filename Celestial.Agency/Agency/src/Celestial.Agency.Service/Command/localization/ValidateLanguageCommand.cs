using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.localization;

public class ValidateLanguageCommandReq : BaseEntity, IRequest<ValidateLanguageCommandRes>
{
    public List<string> Language { get; set; } = new();
}

public class ValidateLanguageCommandRes : ServiceResContextBase
{
}