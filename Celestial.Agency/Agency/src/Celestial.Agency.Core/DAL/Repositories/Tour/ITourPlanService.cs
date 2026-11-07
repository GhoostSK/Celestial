using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Tour;

public interface ITourPlanService : IRepositoryEF<TourPlan>, IScopeLifetime
{
}