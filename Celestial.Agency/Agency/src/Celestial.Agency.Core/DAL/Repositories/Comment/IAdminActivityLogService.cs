using Celestial.Agency.Core.DAL.Model.Global;
using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;
using MofidApp.SharedKernel.DependencyInjection;

namespace Celestial.Agency.Core.DAL.Repositories.Comment;

public interface IAdminActivityLogService : IRepositoryEF<AdminActivityLog>, IScopeLifetime
{
}