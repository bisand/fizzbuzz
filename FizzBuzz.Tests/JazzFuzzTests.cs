using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules.PreDefined;

namespace FizzBuzz.Tests;

public class JazzFuzzTests
{
    private readonly RulesProcessor _jazzFuzzProcessor;

    public JazzFuzzTests()
    {
        _jazzFuzzProcessor = new RulesProcessor(new RuleParser[] { new(new Jazz()), new(new Fuzz()) });
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(4, "Fuzz")]
    [InlineData(9, "Jazz")]
    [InlineData(36, "JazzFuzz")]
    [InlineData(72, "JazzFuzz")]
    [InlineData(100, "Fuzz")]
    public void FizzBuzz_Number_ParsedResult(int number, string expectedResult)
    {
        var processedResult = _jazzFuzzProcessor.ProcessSingle(number);
        Assert.Equal(expectedResult, processedResult);
    }
}