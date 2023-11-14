using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules;
using Microsoft.Extensions.Hosting;

namespace FizzBuzz.App;

public class ConsoleService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifeTime;
    private readonly RuleResolver _ruleResolver;

    public ConsoleService(IHostApplicationLifetime appLifeTime, RuleResolver ruleResolver)
    {
        _appLifeTime = appLifeTime;
        _ruleResolver = ruleResolver;
    }

    /// <summary>
    /// Start running the rules. Rules and ranges could be given by the console app's arguments
    /// </summary>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifeTime.ApplicationStarted.Register(() =>
        {
            Task.Run(() =>
            {
                try
                {
                    var processFizzBuzz = new RulesProcessor(new RuleParser[] { _ruleResolver(3, "Fizz"), _ruleResolver(5, "Buzz") });
                    var processJazzFuzz = new RulesProcessor(new RuleParser[] { _ruleResolver(9, "Jazz"), _ruleResolver(4, "Fuzz") });
                    var fizzBuzz = processFizzBuzz.ProcessRange(1, 100);
                    var jazzFuzz = processJazzFuzz.ProcessRange(100, 1);
                    fizzBuzz.ForEach(Console.WriteLine);
                    jazzFuzz.ForEach(Console.WriteLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error ocurred while executing code {ex}");
                }
                finally
                {
                    _appLifeTime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}