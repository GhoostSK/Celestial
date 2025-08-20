using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Celestial.Agency.Api.Identity.Services.UserIdModelBinder;

public class FromTokenUserIdAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource => BindingSource.Custom;
}

public class TokenUserIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var userIdStr = bindingContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userIdStr, out var userId))
            bindingContext.Result = ModelBindingResult.Success(userId);
        else
            bindingContext.Result = ModelBindingResult.Failed();

        return Task.CompletedTask;
    }
}

public class TokenUserIdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata is DefaultModelMetadata metadata &&
            metadata.Attributes.PropertyAttributes?.OfType<FromTokenUserIdAttribute>().Any() == true)
            return new BinderTypeModelBinder(typeof(TokenUserIdModelBinder));

        return null!;
    }
}