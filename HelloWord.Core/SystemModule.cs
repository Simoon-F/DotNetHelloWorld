using Autofac;
using HelloWord.Core.Settings;
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
    }

    private void RegisterSettings(ContainerBuilder builder)
    {
        var settingTypes = typeof(SystemModule).Assembly.GetTypes()
            .Where(t => t.IsClass && typeof(IConfigurationSetting).IsAssignableFrom(t))
            .ToArray();

        builder.RegisterTypes(settingTypes).AsSelf().SingleInstance();
    }
}