using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Celestial.WebClient.TagHelper;

[HtmlTargetElement("image", Attributes = "path")]
public class ImageUrlTagHelper(IConfiguration config) : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
    [HtmlAttributeName("path")] public string Path { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var baseUrl = config["ApiBaseUrl"];
        var fullUrl = $"{baseUrl}/api/en/Global/ReadFile?filePath={Path}";

        output.TagName = "img";
        output.TagMode = TagMode.SelfClosing;
        output.Attributes.SetAttribute("src", fullUrl);
    }
}