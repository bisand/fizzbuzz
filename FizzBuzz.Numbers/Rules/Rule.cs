namespace FizzBuzz.Numbers.Rules;

public interface IRule
{
    int Divisor { get; }
    string Name { get; }
}

public record Rule(int Divisor, string Name) : IRule;
