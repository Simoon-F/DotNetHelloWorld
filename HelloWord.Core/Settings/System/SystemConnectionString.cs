using Microsoft.Extensions.Configuration;
 
 namespace HelloWord.Core.Settings.System;
 
 public class SystemConnectionString: IConfigurationSetting
 {
     public string Mysql { get; set; }

     public SystemConnectionString(IConfiguration configuration)
     {
         Mysql = configuration.GetConnectionString("Mysql") ?? "";
     }
 }