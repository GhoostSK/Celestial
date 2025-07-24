using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.Agency.Core.DAL.Model.Tour;

public sealed class TourPlans : DbBaseEntity<long>
{
    public long TourId { get; set; }
    public List<long> PlanPictureId { get; set; } = new();
}