using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

public class UpdateBlogCommandReq : BaseEntity, IRequest<UpdateBlogCommandRes>
{
    public int Id { get; set; }
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
}

public class UpdateBlogCommandRes : ServiceResContextBase
{
}