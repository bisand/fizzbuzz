using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FizzBuzz.App;

public delegate RuleParser RuleResolver(int number, string key);

static class Program
{
    /// <summary>
    /// The main entrypoint of the console app.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();

        await host.RunAsync();
    }

    /// <summary>
    /// Create HostBuilder to be used in dependency injection.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));
        builder.ConfigureServices((services) =>
        {
            services.AddLogging(configure =>
            {
                configure.AddConsole().AddDebug();
            });
            services.AddHostedService<ConsoleService>();
            // Services
            services.AddTransient<RuleResolver>(serviceProvider => (number, key) =>
            {
                var result = ActivatorUtilities.CreateInstance<RuleParser>(serviceProvider, number, key);
                return result ?? throw new ArgumentException($"Unable to create rule based on key:{key} and number: {number}");
            });
        });
        return builder;
    }
}