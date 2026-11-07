using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Newsletter;

public sealed class NewsletterSubscription : DbBaseEntity<long>
{
    public string Email { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UnsubscribedAt { get; set; }
    public bool IsDeleted { get; set; }
}
