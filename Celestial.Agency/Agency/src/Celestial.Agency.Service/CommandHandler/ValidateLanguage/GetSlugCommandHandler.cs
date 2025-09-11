using Celestial.Agency.Core.DAL.Repositories.Global;
using Celestial.Agency.Service.Command.localization;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.Agency.Service.CommandHandler.ValidateLanguage;

public sealed class GetSlugCommandHandler(
    IMediator mediator,
    ILocalizedPropertyService localizedPropertyService)
    : BaseRequestHandler<GetSlugCommandReq, GetSlugCommandRes>
{
    protected override async Task<GetSlugCommandRes> HandleCore(GetSlugCommandReq request,
        CancellationToken cancellationToken)
    {
        var langs = new List<string> { request.Language };
        var validateLanguage =
            await mediator.Send(new ValidateLanguageCommandReq { Language = langs }, cancellationToken);
        if (validateLanguage.HasError)
            return new GetSlugCommandRes
            {
                Error = validateLanguage.Error
            };
        var data = await localizedPropertyService.FirstOrDefaultAsync(
            p => p.LocalKey == "Slug"
                 && p.LocalKeyGroup == request.KeyGroup
                 && p.LocalValue.ToLower() == request.Slug.ToLower(),
            cancellationToken: cancellationToken);


        if (data == null)
            return new GetSlugCommandRes
            {
                Error = new ValidationError("404", $"{request.KeyGroup.ToString()} not found with slug")
            };
        if (data.Language == request.Language) return new GetSlugCommandRes();

        var correctItem = await localizedPropertyService.FirstOrDefaultAsync(p =>
                p.EntityId == data.EntityId &&
                p.LocalKey == data.LocalKey &&
                p.EntityId == data.EntityId &&
                p.Language == request.Language &&
                p.LocalKeyGroup == data.LocalKeyGroup,
            cancellationToken: cancellationToken);

        return new GetSlugCommandRes
        {
            IsCorrect = false,
            Slug = correctItem.LocalValue
        };
    }
}