# FizzBuzz.Numbers
## Background
This library is mainly intended to create scenarios for the mathematical challenge known as FizzBuzz

You can easily create your own pre-defined rules like the examples in the namespace `FizzBuzz.Numbers.Rules` or you can dynamically create them by using the `RuleParser`.

## Implementation
Add the project FizzBuzz.Numbers as a project reference
``` console
dotnet add reference ../FizzBuzz.Numbers/FizzBuzz.Numbers.csproj
```
NuGet package will be published in the near future.

## Examples
You can add more pre-defined by inheriting the `RuleParser` class. The example below display how to implement a new `Kizz` rule
``` csharp
public class Kizz : RuleParser
{
    public Kizz() : base(2, nameof(Kizz)) { }
}
```

To dynamically create new rules please instantiate the `RuleParser`class as shown in the example below.
``` csharp
var kizzRule = new RuleParser(2, "Kizz")
```

To process the rules you can use the `RulesProcessor` class. This class takes a list of rules in its constructor and returns the processed results by calling `GetResult`method which also takes a range of numbers to use in the processing. By calling this method you will receive a list of strings containing the processed result based on the defined rules and provided number range.
``` csharp
var result = new RulesProcessor(new RuleParser[] { new(new Fizz()), new(new Buzz()) }).ProcessRange(1, 100);
```

See example below to see how easy you can implement FizzBuzz using a simple [console app](../FizzBuzz.App.Simple/).
``` csharp
using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules;

var result = new RulesProcessor(new RuleParser[] { new(new Fizz()), new(new Buzz()) }).ProcessRange(1, 100);
result.ForEach(item => Console.WriteLine(item));
```

To discuss the implementation and the usage of the library, please go to [GitHub's discussion area](https://github.com/bisand/fizzbuzz/discussions).