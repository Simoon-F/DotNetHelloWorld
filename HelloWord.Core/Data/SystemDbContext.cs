using HelloWord.Core.Settings.System;
using Microsoft.EntityFrameworkCore;

namespace HelloWord.Core.Data;

public class SystemDbContext: DbContext,IUnitOfWork
{
    private readonly SystemConnectionString _connectionString;

    public bool ShouldSaveChanges { get; set; }

    public SystemDbContext(SystemConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 调用了ChangeTracker的DetectChanges方法，它会检测上下文中所有实体的更改。这是为了确保在保存之前，上下文中的实体状态都是正确的
        ChangeTracker.DetectChanges();

        return await base.SaveChangesAsync(cancellationToken);
    }

    // OnConfiguring 是 DbContext的一个重写方法，用于配置数据库连接等信息
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 这一行配置了数据库连接和指定了 MySQL 服务器的版本。
        // _connectionString.Mysql 是一个用于连接到 MySQL 数据库的连接字符串。
        // new MySqlServerVersion(new Version(8, 0, 25)) 指定了所使用的 MySQL 服务器版本，这对于 EF Core 来说是很重要的，以便正确地生成与数据库服务器版本兼容的 SQL 语句
        optionsBuilder.UseMySql(_connectionString.Mysql, new MySqlServerVersion(new Version(5, 7, 36)))
            // 这一行启用了日志记录。它会将 EF Core 生成的 SQL 语句和其他一些日志信息输出到控制台，方便调试和监控数据库交互
            .LogTo(Console.WriteLine)
            // 使用蛇形命名约定，即将数据库表和列的名称从PascalCase（首字母大写）转换为snake_case（小写，用下划线分隔单词）
            .UseSnakeCaseNamingConvention();
    }

    // 这个方法的主要作用是应用来自指定程序集实现了所有的 IEntityTypeConfiguration<TEntity> 接口的配置类
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 从 SystemModule 类所在的程序集中加载所有符合条件的配置类
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemModule).Assembly);
    }
}