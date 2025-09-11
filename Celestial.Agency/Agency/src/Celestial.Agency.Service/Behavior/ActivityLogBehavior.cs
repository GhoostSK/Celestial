//using System.Security.Claims;
//using System.Text.Json;
//using Celestial.Agency.Core.DAL.Model.Global;
//using Celestial.Agency.Core.DAL.Repositories.Comment;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using MofidApp.SharedKernel.Domain.Services;

//namespace Celestial.Agency.Service.Behavior;

//public sealed class ActivityLogBehavior<TRequest, TResponse>(
//    IAdminActivityLogService activityLogService,
//    IHttpContextAccessor httpContextAccessor)
//    : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : notnull, IRequest<TResponse>
//{
//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        // قبل از اجرا می‌تونی اطلاعات request رو بگیری
//        var response = await next();

//        try
//        {
//            var userId = httpContextAccessor.HttpContext?.User
//                .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

//            var userName = httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous";


//            await activityLogService.AddAsync(new AdminActivityLog
//            {
//                UserId = userId,
//                Action = typeof(TRequest).Name,
//                EntityType = InferEntityType(request),
//                EntityId = TryGetEntityId(request),
//                Details = JsonSerializer.Serialize(request)
//            }, cancellationToken);
//            await activityLogService.UnitOfWork.SaveChangesAsync(cancellationToken);
//        }
//        catch
//        {
//            // لاگینگ نباید جلوی پروسه رو بگیره → خطا swallow شه
//        }

//        return response;
//    }

//    private string InferEntityType(TRequest request)
//    {
//        var typeName = typeof(TRequest).Name;
//        if (typeName.Contains("Blog")) return "Blog";
//        if (typeName.Contains("Comment")) return "Comment";
//        if (typeName.Contains("Tour")) return "Tour";
//        return "General";
//    }

//    private long? TryGetEntityId(TRequest request)
//    {
//        var prop = request.GetType().GetProperty("EntityId");
//        if (prop != null)
//        {
//            var val = prop.GetValue(request);
//            if (val != null && long.TryParse(val.ToString(), out var id))
//                return id;
//        }

//        return null;
//    }
//}

//public sealed class AuditLoggingBehavior<TRequest, TResponse>
//    : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TResponse : ServiceResContextBase
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly IAdminActivityLogService _logService;

//    public AuditLoggingBehavior(
//        IAdminActivityLogService logService,
//        IHttpContextAccessor httpContextAccessor)
//    {
//        _logService = logService;
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        // اول بفرست بره سمت Handler
//        var response = await next();

//        // بعد از اجرای Handler میایم اینجا
//        var attr = typeof(TRequest).GetCustomAttributes(typeof(AuditLogAttribute), false)
//            .FirstOrDefault() as AuditLogAttribute;

//        // اگه Attribute نداشت → رد شو
//        if (attr == null)
//            return response;

//        // اگه خطا داشت → لاگ نزن
//        if (response.HasError)
//            return response;

//        // اطلاعات کاربر
//        var user = _httpContextAccessor.HttpContext?.User;
//        var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        var userName = user?.Identity?.Name ?? "";

//        // لاگ زدن
//        await _logService.AddAsync(new AdminActivityLog
//        {
//            UserId = userId ?? "",
//            UserName = userName,
//            Action = attr.Action,
//            EntityType = attr.EntityType,
//            EntityId = (request as IEntityCommand)?.EntityId,
//            Details = request.ToString()
//        }, cancellationToken);

//        return response;
//    }
//}

//[AttributeUsage(AttributeTargets.Class)]
//public class AuditLogAttribute(string action, string entityType) : Attribute
//{
//    public string Action { get; set; } = action;
//    public string EntityType { get; set; } = entityType;
//}

