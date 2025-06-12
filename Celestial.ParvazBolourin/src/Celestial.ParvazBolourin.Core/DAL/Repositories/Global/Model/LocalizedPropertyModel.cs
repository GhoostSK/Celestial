using Celestial.ParvazBolourin.Core.DAL.Model.Global;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;

public sealed class GetLocalizedPropertyReq
{
    public int LanguageId { get; set; }
    public long EntityId { get; set; }
    public LocalKeyGroupType LocalKeyGroup { get; set; }
}

public sealed class GetLocalizedPropertyRes
{
    public long Id { get; set; }
    public string LocalKey { get; set; } = string.Empty;
    public string LocalValue { get; set; } = string.Empty;
    public LocalKeyGroupType LocalKeyGroup { get; set; }
    public int LanguageId { get; set; }
}