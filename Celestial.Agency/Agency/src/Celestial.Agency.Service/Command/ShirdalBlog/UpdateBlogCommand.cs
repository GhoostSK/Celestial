using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.ShirdalBlog;

public class UpdateBlogCommandReq : BaseEntity, IRequest<UpdateBlogCommandRes>
{
    public int Id { get; set; }
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
}

public class UpdateBlogCommandRes : ServiceResContextBase
{
}