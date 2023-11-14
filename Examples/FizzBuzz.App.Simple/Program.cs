using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules.PreDefined;

var fizzBuzz = new RulesProcessor(new RuleParser[] { new(new Fizz()), new(new Buzz()) }).ProcessRange(1, 100);
var jazzFuzz = new RulesProcessor(new RuleParser[] { new(new Jazz()), new(new Fuzz()) }).ProcessRange(100, 1);
fizzBuzz.Concat(jazzFuzz).ForEach(Console.WriteLine);
