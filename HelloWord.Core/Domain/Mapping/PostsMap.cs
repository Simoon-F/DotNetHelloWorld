using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloWord.Core.Domain.Mapping;

/// <summary>
/// Entity Framework Core 配置：
/// 
/// 用于定义实体与数据库表之间的映射关系。
/// 在数据库中创建表格时，指定表名、主键、列名等信息。
/// 配置实体属性与数据库列之间的映射规则。
/// PostsMap 通过 IEntityTypeConfiguration<Posts.Posts/> 接口配置了 Domain.Posts.Posts 实体与数据库表的映射关系。
///
/// Entity Framework Core 配置是为了将实体与数据库表进行关联
/// </summary>
public class PostsMap: IEntityTypeConfiguration<Posts.Posts>
{
    public void Configure(EntityTypeBuilder<Posts.Posts> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(t => t.Id);
        // builder.Property(t => t.Title).HasColumnName("title");
        // builder.Property(t => t.Content).HasColumnName("content");
        // builder.Property(t => t.CreateAt).HasColumnName("create_at");
        // builder.Property(t => t.UpdateAt).HasColumnName("update_at");
    }
}