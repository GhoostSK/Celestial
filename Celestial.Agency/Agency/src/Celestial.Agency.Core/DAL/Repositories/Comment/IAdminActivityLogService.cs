using Celestial.Agency.Core.DAL.Model.Global;
using Celestial.Dal.Ef.SqlServer.Abstractions;
using Celestial.Dal.Ef.SqlServer.Extensions;

namespace Celestial.Agency.Core.DAL.Repositories.Comment;

public interface IAdminActivityLogService : IRepositoryEF<AdminActivityLog>, IScopeLifetime
{
}