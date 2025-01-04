using Celestial.ParvazBolourin.Service.Command.ShirdalBlog;

namespace Celestial.ParvazBolourin.Api.Areas.Admin.Models.Dto;

public class UpdateBlogReqDto
{
    public int Id { get; set; }
    public List<BlogLocalizedProperty> BlogLocalizedProperties { get; set; } = new();
    public string PicturePath { get; set; } = string.Empty;
}