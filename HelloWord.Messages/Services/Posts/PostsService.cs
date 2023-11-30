using AutoMapper;
using HelloWord.Messages.Requests.Posts;

namespace HelloWord.Messages.Services.Posts;

public partial class PostsService: IPostsService
{
    private readonly IMapper _mapper;
    
    public PostsService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<GetPostsListResponse> GetPostsListAsync(GetPostsListRequest request,
        CancellationToken cancellationToken)
    {
        var list = new List<GetPostsListResponseDto>
        {
            new GetPostsListResponseDto
            {
                Id = 1,
                Title = "test",
                Context = "Context",
                CreateAt = new DateTimeOffset().DateTime
            }
        };

        return new GetPostsListResponse()
        {
            Data = _mapper.Map<List<GetPostsListResponseDto>>(list)
        };
    }
    
}