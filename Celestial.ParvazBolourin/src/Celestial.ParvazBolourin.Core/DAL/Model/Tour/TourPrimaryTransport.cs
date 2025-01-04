using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public sealed class TourPrimaryTransport : DbBaseEntity<int>
{
    public string LeadFlightCode { get; set; } = string.Empty;
    public string ReturnFlightCode { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string LeadStartTime { get; set; } = string.Empty;
    public string LeadEndTime { get; set; } = string.Empty;
    public string ReturnStartTime { get; set; } = string.Empty;
    public string ReturnEndTimeTime { get; set; } = string.Empty;
}