using FizzBuzz.Numbers.Rules;

namespace FizzBuzz.Numbers;

public class RuleParser
{
    /// <summary>
    /// Rule to be matched
    /// </summary>
    /// <value></value>
    public IRule Rule { get; }

    /// <summary>
    /// Provide a rule that will represent the value when a parsed number is divisible with the divisor.
    /// </summary>
    /// <param name="divisor">Divisor to be devided against the parsed number.</param>
    /// <param name="name">Word that will represent the number if parsed number is divisible against the divisor.</param>
    public RuleParser(IRule rule) => Rule = rule;

    /// <summary>
    /// Provide a divisor and a word that will represent the value when a parsed number is divisible with the divisor.
    /// </summary>
    /// <param name="divisor"></param>
    /// <param name="name"></param>
    public RuleParser(int divisor, string name) => Rule = new Rule(divisor, name);

    /// <summary>
    /// Check if the provided number is divisible against the defined divisor
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public bool IsMatch(int number) => (Rule.Divisor != 0 && number % Rule.Divisor == 0);

    /// <summary>
    /// Parse a number and return the word when the provided number is divisible against the defined divisor.
    /// </summary>
    /// <param name="number">Number to be devided against the defined divisor.</param>
    /// <returns>Return the defined word or the provided number depending on the value of the number.</returns>
    public string Parse(int number) => IsMatch(number) ? Rule.Name : number.ToString();
}