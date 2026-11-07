using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Tour;

public sealed class TourPrimaryTransport : DbBaseEntity<long>
{
    public long TourId { get; set; }
    public Tour Tour { get; set; }
    public string LeadFlightCode { get; set; } = string.Empty;
    public string ReturnFlightCode { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string LeadStartTime { get; set; } = string.Empty;
    public string LeadEndTime { get; set; } = string.Empty;
    public string ReturnStartTime { get; set; } = string.Empty;
    public string AirLine { get; set; } = string.Empty;
    public string AirLineImagePath { get; set; } = string.Empty;
    public string ReturnEndTimeTime { get; set; } = string.Empty;
}