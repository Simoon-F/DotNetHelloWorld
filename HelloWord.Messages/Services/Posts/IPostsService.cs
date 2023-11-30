using HelloWord.Messages.Ioc;
using HelloWord.Messages.Requests.Posts;

namespace HelloWord.Messages.Services.Posts;

public interface IPostsService: IScopedDependency
{
    Task<GetPostsListResponse> GetPostsListAsync(GetPostsListRequest request, CancellationToken cancellationToken);
}