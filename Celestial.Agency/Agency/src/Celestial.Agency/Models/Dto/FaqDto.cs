using System.ComponentModel.DataAnnotations;
using Celestial.Agency.Service.Command.Faq;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class GetFaqListReqDto
{
    [Required] [FromRoute] public string Language { get; set; } = string.Empty;
}

public sealed class AddFaqReqDto
{
    public int DisplayOrder { get; set; }
    public bool Published { get; set; } = true;

    [Required] [MinLength(1)] public List<FaqLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class UpdateFaqReqDto
{
    [Required] public long Id { get; set; }

    public int DisplayOrder { get; set; }
    public bool Published { get; set; } = true;
    public List<FaqLocalizedPropertyItem> LocalizedProperty { get; set; } = new();
}

public sealed class RemoveFaqReqDto
{
    [Required] public long Id { get; set; }
}
