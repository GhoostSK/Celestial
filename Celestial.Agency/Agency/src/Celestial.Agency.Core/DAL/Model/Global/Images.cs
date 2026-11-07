using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Global;

public class Image : DbBaseEntity<long>
{
    public string KeyGroup { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}