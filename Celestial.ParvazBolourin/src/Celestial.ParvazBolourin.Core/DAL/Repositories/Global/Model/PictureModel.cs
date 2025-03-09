using System.ComponentModel.DataAnnotations;

namespace Celestial.ParvazBolourin.Core.DAL.Repositories.Global.Model;

public sealed class PictureItem
{
    [Required] public string FilePath { get; set; } = string.Empty;

    [Required] public int DisplayOrder { get; set; }
}