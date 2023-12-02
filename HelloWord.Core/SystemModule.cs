using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using HelloWord.Core.Data;
using HelloWord.Core.DbUp;
using HelloWord.Core.Ioc;
using HelloWord.Core.Settings;
using Mediator.Net;
using Mediator.Net.Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWord.Core;

public class SystemModule: Module
{
    private readonly IConfiguration _configuration;

    public SystemModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        RegisterSettings(builder);
        RegisterDatabase(builder);
        RegisterMediator(builder);
        RegisterDependency(builder);
        RegisterAutoMapper(builder);
    }
    
    private void RegisterDependency(ContainerBuilder builder)
    {
        foreach (var type in typeof(IDependency).Assembly.GetTypes()
                     .Where(type => type.IsClass && typeof(IDependency).IsAssignableFrom(type)))
        {
            if (typeof(IScopedDependency).IsAssignableFrom(type))
                builder.RegisterType(type).AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }

    private void RegisterSettings(ContainerBuilder builder)
    {
        var settingTypes = typeof(SystemModule).Assembly.GetTypes()
            .Where(t => t.IsClass && typeof(IConfigurationSetting).IsAssignableFrom(t))
            .ToArray();

        // AsSelf ： 表示使用类型自身作为服务类型。这意味着每个类型都可以通过其自身类型进行解析
        // SingleInstance ：表示注册的组件是单例的，即在整个应用程序生命周期中只创建一个实例
        builder.RegisterTypes(settingTypes).AsSelf().SingleInstance();
    }

    private void RegisterMediator(ContainerBuilder builder)
    {
        // 创建了一个 MediatorBuilder 的实例，用于配置 MediatR
        var mediatorBuilder = new MediatorBuilder();

        // 注册了 MediatR 处理程序。typeof(SystemModule).Assembly 表示使用 SystemModule 类所在的程序集中的所有类型作为处理程序
        mediatorBuilder.RegisterHandlers(typeof(SystemModule).Assembly);
        
        // 配置了 MediatR 的全局接收管道（receive pipe）。在这里，定义了一系列中间件，它们将按照指定的顺序处理每个消息
        mediatorBuilder.ConfigureGlobalReceivePipe(c =>
        {
            // 根据需要配置是否使用 Serilog 中间件
            // 可以根据消息类型进行不同的配置
        });
        
        // 使用中间件的地方
        
        // 最后，将配置好的 MediatorBuilder 注册到 Autofac 容器中，使得 MediatR 可以在应用程序中使用
        builder.RegisterMediator(mediatorBuilder);
    }

    private void RegisterAutoMapper(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(typeof(SystemModule).Assembly);
    }

    private void RegisterDatabase(ContainerBuilder builder)
    {
        builder.RegisterType<SystemDbContext>()
            .AsSelf() // 这表示将 SystemDbContext 注册为自身类型，这意味着当请求 SystemDbContext 时，容器将返回该类型的一个实例
            .As<DbContext>() // 将 SystemDbContext 注册为 DbContext 类型，这是 Entity Framework Core 上下文类的基类。这有助于在应用程序中需要 DbContext 的地方使用 SystemDbContext
            .AsImplementedInterfaces() // 注册 SystemDbContext 实现的所有接口。这允许在应用程序中通过这些接口来引用 SystemDbContext。
            .InstancePerLifetimeScope(); // 这表示容器将为每个生命周期范围（通常是 HTTP 请求的生命周期）创建一个实例。这确保了在同一个请求期间多次请求 SystemDbContext 时，都会获得相同的实例
        
        // 注册了一个泛型仓库 EfRepository，并将其注册为 IRepository 接口。这种方式使得在应用程序中，
        builder.RegisterType<EfRepository>().As<IRepository>() // 通过依赖注入引用 IRepository 时，将会获得 EfRepository 的实例。
            .InstancePerLifetimeScope(); // 同样，这表示 EfRepository 将在每个生命周期范围内创建一个实例。
    }
}