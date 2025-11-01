using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.localization;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Mediator;

namespace Celestial.Agency.Service.CommandHandler.ValidateLanguage;

public sealed class ValidateLanguageCommandHandler(ILanguageService languageService)
    : BaseRequestHandler<ValidateLanguageCommandReq, ValidateLanguageCommandRes>
{
    protected override async Task<ValidateLanguageCommandRes> HandleCore(ValidateLanguageCommandReq request,
        CancellationToken cancellationToken)
    {
        var languages = await languageService.GetLanguages(cancellationToken);
        foreach (var item in request.Language)
            if (languages.All(p => p.LanguageCulture.Split('-')[0] != item))

                return new ValidateLanguageCommandRes
                {
                    Error = new ValidationError("LA400", $"invalid Language {item}")
                };
        return new ValidateLanguageCommandRes();
    }
}