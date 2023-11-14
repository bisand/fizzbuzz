using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules.PreDefined;

namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    private readonly RulesProcessor _fizzBuzzProcessor;

    public FizzBuzzTests()
    {
        _fizzBuzzProcessor = new RulesProcessor(new RuleParser[] { new(new Fizz()), new(new Buzz()) });
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    [InlineData(75, "FizzBuzz")]
    [InlineData(100, "Buzz")]
    public void FizzBuzz_Number_ParsedResult(int number, string expectedResult)
    {
        var processedResult = _fizzBuzzProcessor.ProcessSingle(number);
        Assert.Equal(expectedResult, processedResult);
    }
}