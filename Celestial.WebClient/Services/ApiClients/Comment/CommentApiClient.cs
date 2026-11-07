using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Extensions;

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
        var query = new ApiQueryBuilder()
            .Add("Slug", request.Slug)
            .Add("LocalGroup", request.LocalGroup.ToString());

        return await httpClient.GetAsync<GetCommentsBySlugResDto>(
            $"/api/{request.Language}/Comment/GetCommentBySlug{query}");
    }
}
