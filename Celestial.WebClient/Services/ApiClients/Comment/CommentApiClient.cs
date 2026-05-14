using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Comment;

public class CommentApiClient(HttpJsonClient httpClient)
{
    public async Task<AddCommentResDto> AddCommentAsync(AddCommentReqDto request)
    {
        return await httpClient.PostAsync<AddCommentReqDto, AddCommentResDto>(
            $"/api/{request.Language}/Comment/AddComment",
            request
        );
    }

    public async Task<GetCommentsBySlugResDto> GetCommentsBySlugAsync(GetCommentsBySlugReqDto request)
    {
        return await httpClient.GetAsync<GetCommentsBySlugResDto>(
            $"/api/fa/Comment/GetCommentBySlug?Slug={Uri.EscapeDataString(request.Slug)}&LocalGroup={request.LocalGroup}"
        );
    }
}