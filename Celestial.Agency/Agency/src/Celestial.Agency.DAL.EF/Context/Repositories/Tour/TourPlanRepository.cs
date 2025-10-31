using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.DAL.Repositories.Tour;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using Celestial.Dal.Ef.SqlServer.Base;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Tour;

public class TourPlanRepository(MainDbContext dbContext) : RepositoryBase<TourPlans>(dbContext), ITourPlanService
{
}