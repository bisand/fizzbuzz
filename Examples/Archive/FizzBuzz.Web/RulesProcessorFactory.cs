using FizzBuzz.Numbers;

public class RulesProcessorFactory
{
    public RulesProcessor GetRulesProcessor(IEnumerable<RuleParser> rules)
    {
        return new RulesProcessor(rules);
    }
}