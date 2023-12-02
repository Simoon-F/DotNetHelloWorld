using AutoMapper;
using HelloWord.Core.DbUp;
using HelloWord.Core.Domain.Posts;
using HelloWord.Core.Providers.Posts;
using HelloWord.Messages.Requests.Posts;
using Microsoft.EntityFrameworkCore;

namespace HelloWord.Core.Services.Posts;

public partial class PostsService: IPostsService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly IPostsDataProvider _postsDataProvider;
    
    public PostsService(IMapper mapper,IRepository repository, PostsDataProvider postsDataProvider)
    {
        _mapper = mapper;
        _repository = repository;
        _postsDataProvider = postsDataProvider;
    }

    public async Task<GetPostsListResponse> GetPostsListAsync(GetPostsListRequest request,
        CancellationToken cancellationToken)
    {
        var postsList =  await _repository.Query<Domain.Posts.Posts>().ToListAsync(cancellationToken).ConfigureAwait(false);

        return  new GetPostsListResponse()
        {
            Data = _mapper.Map<List<GetPostsListResponseDto>>(postsList)
        };
        
    }
    
}