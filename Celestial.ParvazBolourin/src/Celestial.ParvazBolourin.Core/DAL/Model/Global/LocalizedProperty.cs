using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Global;

public sealed class LocalizedProperty : DbBaseEntity<long>
{
    public LocalKeyGroupType LocalKeyGroup { get; set; }
    public string LocalKey { get; set; } = string.Empty;
    public string LocalValue { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    public long EntityId { get; set; }
}

public enum LocalKeyGroupType
{
    Hotel,
    Blog,
    BlogCategory,
    BlogTag,
    Tour,
    TourPlan,
    Visa,
    TourEquipment
}