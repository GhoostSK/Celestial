using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Faq;

/// <summary>
///     پرسش‌های متداول. متن پرسش و پاسخ در <c>LocalizedProperty</c> با گروه
///     <c>LocalKeyGroupType.Faq</c> و کلیدهای <c>Question</c> و <c>Answer</c> نگهداری می‌شود.
/// </summary>
public sealed class Faq : DbBaseEntity<long>
{
    public int DisplayOrder { get; set; }
    public bool Published { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
