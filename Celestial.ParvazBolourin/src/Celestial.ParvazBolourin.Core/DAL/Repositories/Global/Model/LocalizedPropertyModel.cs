namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;

public sealed class GetLocalizedPropertyReq
{
    public int LanguageId { get; set; }
    public long EntityId { get; set; }
    public string LocalKeyGroup { get; set; } = string.Empty;
}

public sealed class GetLocalizedPropertyRes
{
    public long Id { get; set; }
    public string LocalKey { get; set; } = string.Empty;
    public string LocalValue { get; set; } = string.Empty;
    public string KeyGroup { get; set; } = string.Empty;
    public int LanguageId { get; set; }
}