using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Extensions;

public static class TransportExtensions
{
    /// <summary>
    ///     آیکون Bootstrap Icons متناسب با نوع وسیله‌ی نقلیه، برای نمایش در نقشه‌ی مسیر تور.
    /// </summary>
    public static string ToTransportIcon(this TransportVehicleType vehicleType)
    {
        return vehicleType switch
        {
            TransportVehicleType.Plane => "bi-airplane",
            TransportVehicleType.Bus => "bi-bus-front",
            TransportVehicleType.Car => "bi-car-front",
            TransportVehicleType.Ship => "bi-water",
            _ => "bi-signpost-split"
        };
    }
}
