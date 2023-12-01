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
        var response =  await _repository.Query<Post>().ToListAsync(cancellationToken).ConfigureAwait(false);

        // var list = new List<GetPostsListResponseDto>
        // {
        //     new GetPostsListResponseDto
        //     {
        //         Id = 1,
        //         Title = "test",
        //         Context = "Context",
        //         CreateAt = new DateTimeOffset(new DateTime(2023, 11, 30, 12, 0, 0), TimeSpan.Zero)
        //     }
        // };
        
        var response2 =  new GetPostsListResponse()
        {
            Data = _mapper.Map<List<GetPostsListResponseDto>>(response)
        };
        
        return response2;
    }
    
}