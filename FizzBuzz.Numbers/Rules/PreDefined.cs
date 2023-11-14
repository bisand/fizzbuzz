namespace FizzBuzz.Numbers.Rules.PreDefined;

public sealed record Fizz() : Rule(3, nameof(Fizz));
public sealed record Buzz() : Rule(5, nameof(Buzz));
public sealed record Jazz() : Rule(9, nameof(Jazz));
public sealed record Fuzz() : Rule(4, nameof(Fuzz));
