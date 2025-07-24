using System.ComponentModel.DataAnnotations;

namespace Celestial.Agency.Core.DAL.Repositories.Global.Model;

public sealed class PictureItem
{
    [Required] public string FilePath { get; set; } = string.Empty;

    [Required] public int DisplayOrder { get; set; }
}