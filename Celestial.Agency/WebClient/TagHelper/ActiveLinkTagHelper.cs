using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Celestial.Agency.WebClient.TagHelper;

[HtmlTargetElement("li", Attributes = "active-if")]
public class ActiveParentLiTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
    [HtmlAttributeName("active-if")] public string ActivePage { get; set; }

    [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var currentPage = ViewContext.ActionDescriptor.DisplayName;

        if (currentPage.Contains(ActivePage, StringComparison.OrdinalIgnoreCase))
        {
            var existingClass = output.Attributes["class"]?.Value?.ToString();
            var newClass = string.IsNullOrEmpty(existingClass) ? "active" : $"{existingClass} active";
            output.Attributes.SetAttribute("class", newClass);
        }
    }
}