using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;
using Omnic.Commands;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddEnvironmentVariables("OMNIC:");

string? token = builder.Configuration.GetValue<string>("Bot:Token");
if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine("No bot token found");
    return;
}

builder.Services
    .AddDiscordGateway(c => c.Token = token)
    .AddApplicationCommands();

var host = builder.Build();

host.AddApplicationCommandModule<OverwatchCommandModule>();

await host.RunAsync();