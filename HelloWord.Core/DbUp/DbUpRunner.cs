using DbUp;
using MySql.Data.MySqlClient;

namespace HelloWord.Core.DbUp;

public class DbUpRunner
{
    // 数据库连接字符串
    private readonly string _connectionString;

    // 构造函数，接受数据库连接字符串作为参数
    public DbUpRunner(string connectionString)
    {
        _connectionString = connectionString;
    }

    // 执行数据库迁移操作
    public void Run()
    {
        // 调用私有方法，创建数据库（如果不存在）
        CreateDatabaseIfNotExist();
        
        // 使用 DbUp 库确保数据库存在
        EnsureDatabase.For.MySqlDatabase(_connectionString);

        // 使用 DbUp 配置数据库升级
        var upgrade = DeployChanges.To.MySqlDatabase(_connectionString) // 表示将数据库升级的目标设置为 MySQL 数据库
            // 用于指定升级所需的脚本嵌入在程序集中，获取了 DbUpRunner 类所在的程序集，从而能够找到嵌入在该程序集中的数据库升级脚本
            .WithScriptsEmbeddedInAssembly(typeof(DbUpRunner).Assembly) 
            .WithTransaction()  // 使用事务，表示在执行升级脚本时使用事务。如果升级中的某个脚本失败，所有之前已经执行的脚本将会回滚，确保数据库的一致性
            .LogToAutodetectedLog()  // 用于自动检测并记录日志。DbUp 可以根据运行时环境选择适当的日志记录方式
            .LogToConsole()  // 表示将日志输出到控制台。这有助于在调试或查看升级过程时查看详细的日志信息
            .Build();  // 用于构建 UpgradeEngine 实例，该实例包含了所有配置信息，可以执行数据库升级操作

        // 执行数据库升级，并获取升级的结果
        var result = upgrade.PerformUpgrade();

        // 如果升级不成功，抛出异常
        if (!result.Successful)
            throw result.Error;
    }

    // 用于创建数据库 （如果不存在）
    private void CreateDatabaseIfNotExist()
    {
        // 调用私有方法，生成创建数据库的 SQL 语句
        var (connStr, databaseName) = GenerateCreateDatabaseIfNotExistSql(_connectionString);
        
        // 使用 MySqlConnection 和 MySqlCommand 来执行 SQL 语句
        using var connection = new MySqlConnection(connStr);
        using var command = new MySqlCommand("CREATE SCHEMA If Not Exists `" + databaseName + "` Character Set UTF8;",
            connection);

        // 尝试打开数据库连接，执行 SQL 语句，最后关闭数据库连接。
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }

    // 生成创建数据库的 SQL 语句， 并返回新的连接字符串和数据库名称
    private (string ConnectionStrin, string Sql) GenerateCreateDatabaseIfNotExistSql(string connectionStr)
    {
        // 使用 MySqlConnectionStringBuilder 解析连接字符串，提取数据库名称。
        var connectionStringBuilder = new MySqlConnectionStringBuilder((connectionStr));
        var database = connectionStringBuilder.Database;
        
        // 将连接字符串中的数据库名称置为空，以便生成不包含具体数据库的连接字符串。
        connectionStringBuilder.Database = null;
        
        // 返回新的连接字符串和提取的数据库名称。
        return (connectionStringBuilder.ToString(), database);
    }
}