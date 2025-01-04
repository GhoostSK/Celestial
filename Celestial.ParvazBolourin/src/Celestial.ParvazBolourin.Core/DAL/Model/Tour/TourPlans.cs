using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.Tour;

public sealed class TourPlans : DbBaseEntity<int>
{
    public int TourId { get; set; }
    public List<string> PlanPicturePath { get; set; } = new();
}