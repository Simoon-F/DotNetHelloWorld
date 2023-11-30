using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using HelloWord.Core.Ioc;
using HelloWord.Core.Settings;
using Mediator.Net;
using Mediator.Net.Autofac;
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
        RegisterMediator(builder);
        RegisterAutoMapper(builder);
        RegisterDependency(builder);
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
}