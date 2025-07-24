using Celestial.Agency.Core.DAL.Model.Tour;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Tour;

public interface ITourPlanService : IRepositoryEF<TourPlans>, IScopeLifetime
{
}