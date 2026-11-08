using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetFaqListReqDto
{
    public string Language { get; set; } = string.Empty;
}

public sealed class GetFaqListResDto : ServiceResContextBase
{
    public List<FaqItemDto> Items { get; set; } = new();
}

public sealed class FaqItemDto
{
    public long Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
