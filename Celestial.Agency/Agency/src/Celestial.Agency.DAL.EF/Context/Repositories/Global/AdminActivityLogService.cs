using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Agency.Core.DAL.Repositories.Comment;
using Celestial.Agency.DAL.EF.Context.DatabaseContext;
using MofidApp.SharedKernel.DAL.EntityFramework;

namespace Celestial.Agency.DAL.EF.Context.Repositories.Global;

public class AdminActivityLogService(MainDbContext dbContext)
    : RepositoryBase<AdminActivityLog>(dbContext), IAdminActivityLogService
{
}