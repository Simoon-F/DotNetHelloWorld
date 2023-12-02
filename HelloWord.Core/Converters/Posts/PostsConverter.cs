using AutoMapper;
using HelloWord.Messages.Requests.Posts;

namespace HelloWord.Core.Converters.Posts;

/// <summary>
/// 用于定义对象之间的映射规则，而不涉及数据库。
/// PostsConverter 配置了 GetPostsListResponseDto 和 Domain.Posts.Posts 之间的映射关系。
/// AutoMapper 的配置用于将一个对象的属性值映射到另一个对象的属性，通常在业务逻辑中使用，而不是与数据库交互。
///
/// 而 AutoMapper 配置是为了在业务逻辑中方便地转换对象。
/// </summary>
public class PostsConverter: Profile
{
    public PostsConverter()
    {
        CreateMap<GetPostsListResponseDto, Domain.Posts.Posts>();
        CreateMap<Domain.Posts.Posts, GetPostsListResponseDto>();
    }
}