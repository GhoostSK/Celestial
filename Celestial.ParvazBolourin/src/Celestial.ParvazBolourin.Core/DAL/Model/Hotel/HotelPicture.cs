using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Hotel;

public sealed class HotelPicture : DbBaseEntity<int>
{
    public int HotelId { get; set; }
    public string VirtualPath { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}