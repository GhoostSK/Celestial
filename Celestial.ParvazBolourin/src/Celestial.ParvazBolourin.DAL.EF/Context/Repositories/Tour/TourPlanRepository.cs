using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.DAL.Repositories.Tour;
using Celestial.ParvazBolourin.DAL.EF.Context.DatabaseContext;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.ParvazBolourin.DAL.EF.Context.Repositories.Tour;

public class TourPlanRepository(MainDbContext dbContext) : RepositoryBase<TourPlans>(dbContext), ITourPlanService
{

}