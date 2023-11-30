using AutoMapper;
using HelloWord.Messages.Requests.Posts;

namespace HelloWord.Core.Services.Posts;

public partial class PostsService: IPostsService
{
    private readonly IMapper _mapper;
    
    public PostsService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<GetPostsListResponse> GetPostsListAsync(GetPostsListRequest request,
        CancellationToken cancellationToken)
    {
        var list = new List<GetPostsListResponseDto>
        {
            new GetPostsListResponseDto
            {
                Id = 1,
                Title = "test",
                Context = "Context",
                CreateAt = new DateTimeOffset(new DateTime(2023, 11, 30, 12, 0, 0), TimeSpan.Zero)
            }
        };

        var response =  new GetPostsListResponse()
        {
            Data = _mapper.Map<List<GetPostsListResponseDto>>(list)
        };
        
        return Task.FromResult(response);
    }
    
}