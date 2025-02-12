using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;

public interface ITourPlanService : IRepositoryEF<TourPlans>, IScopeLifetime
{
}