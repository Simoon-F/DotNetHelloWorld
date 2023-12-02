using Mediator.Net.Contracts;

namespace HelloWord.Messages.Requests.Posts;

public class GetPostsListRequest: IRequest
{
    
}

public class GetPostsListResponse : IResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = "";
    public List<GetPostsListResponseDto> Data { get; set; }
}

public class GetPostsListResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Context { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public DateTimeOffset UpdateAt { get; set; }
}