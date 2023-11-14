using System.Collections.Immutable;
using FizzBuzz.Numbers.Rules;

namespace FizzBuzz.Numbers;

/// <summary>
/// Rules processor that is defined with a set of rules and processes the results based on a range of numbers.
/// This class can not be inherited.
/// </summary>
public class RulesProcessor
{
    /// <summary>
    /// A list of read only rules to be used in the processor.
    /// Each rule is an implementation of RuleParser.
    /// The rules must be provided in the constructor.
    /// </summary>
    /// <value></value>
    public IEnumerable<RuleParser> RuleParsers { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ruleParsers">List of rules of type RuleParser</param>
    /// <returns></returns>
    public RulesProcessor(IEnumerable<RuleParser> ruleParsers) => RuleParsers = ruleParsers;

    /// <summary>
    /// Responsible for processing a single number and returning the correct result. 
    /// All depending on the defined rules and the provided number.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public string ProcessSingle(int number)
    {
        return RuleParsers.Where(rule => rule.IsMatch(number))
            .Select(rule => rule.Parse(number))
            .DefaultIfEmpty(number.ToString())
            .Aggregate((x, y) => $"{x}{y}");
    }

    /// <summary>
    /// Get the result from the parsed rules. The rules are parsed using the provided range of numbers.
    /// Note! The parameters can be both negative and positive. Ascending and descending.
    /// </summary>
    /// <param name="from">Number from</param>
    /// <param name="to">Number to</param>
    /// <returns>A list of strings</returns>
    public IEnumerable<string> ProcessRange(int from, int to)
    {
        return (from, to).EnumerateRange().Select(ProcessSingle);
    }
}