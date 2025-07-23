using Celestial.ParvazBolourin.Core.DAL.Repositories.Global;
using Celestial.ParvazBolourin.Service.Command.ValidateLanguage;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Service.CommandHandler.ValidateLanguage;

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
        return new ValidateLanguageCommandRes { };
    }
}