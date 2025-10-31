using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.ShirdalBlog;

public sealed class Blog : DbBaseEntity<long>
{
    public int Likes { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public long CategoryId { get; set; }
    public List<long> TagsId { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
}