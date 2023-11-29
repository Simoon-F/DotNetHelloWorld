using Autofac;
using Autofac.Extensions.DependencyInjection;
using HelloWord.Core;
using HelloWord.Core.DbUp;
using HelloWord.Core.Settings.System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mysql = new SystemConnectionString(builder.Configuration).Mysql;

if (mysql is not null)
{
    new DbUpRunner(mysql).Run();
}

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new SystemModule(builder.Configuration))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();