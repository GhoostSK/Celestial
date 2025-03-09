using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Global;

public sealed class Comments : DbBaseEntity<long>
{
    public int EntityId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = false;
}