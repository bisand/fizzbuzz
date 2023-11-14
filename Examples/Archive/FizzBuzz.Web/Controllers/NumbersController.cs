using FizzBuzz.Numbers;
using FizzBuzz.Numbers.Rules.PreDefined;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        public NumbersController(RulesProcessorFactory rulesProcessorFactory)
        {
            _rulesProcessorFactory = rulesProcessorFactory;
        }

        private RulesProcessorFactory _rulesProcessorFactory { get; }

        [HttpGet]
        [Route("fizzbuzz")]
        public IEnumerable<string> GetFizzBuzz([FromQuery] int from = 1, [FromQuery] int to = 100)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(new RuleParser[] { new RuleParser(new Fizz()), new RuleParser(new Buzz()) });
            var result = proecessor.ProcessRange(from, to);
            return result;
        }

        [HttpGet]
        [Route("fizzbuzz-full")]
        public IEnumerable<NumberRule> GetFizzBuzzFull([FromQuery] int from = 1, [FromQuery] int to = 100)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(new RuleParser[] { new RuleParser(new Fizz()), new RuleParser(new Buzz()) });
            var counter = from;
            var result = proecessor.ProcessRange(from, to).Select(res => new NumberRule { Number = (from < to ? counter++ : counter--), Word = res });
            return result;
        }

        [HttpGet]
        [Route("jazzfuzz")]
        public IEnumerable<string> GetJazzFuzz([FromQuery] int from = 100, [FromQuery] int to = 1)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(new RuleParser[] { new RuleParser(new Jazz()), new RuleParser(new Fuzz()) });
            var result = proecessor.ProcessRange(from, to);
            return result;
        }

        [HttpGet]
        [Route("jazzfuzz-full")]
        public IEnumerable<NumberRule> GetJazzFuzzFull([FromQuery] int from = 100, [FromQuery] int to = 1)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(new RuleParser[] { new RuleParser(new Jazz()), new RuleParser(new Fuzz()) });
            var counter = from;
            var result = proecessor.ProcessRange(from, to).Select(res => new NumberRule { Number = (from < to ? counter++ : counter--), Word = res });
            return result;
        }

        [HttpPost]
        [Route("custom")]
        public IEnumerable<string> GetCustom([FromBody] IEnumerable<NumberRule> rules, [FromQuery] int from = 100, [FromQuery] int to = 1)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(rules.Select(rule=> new RuleParser(rule.Number, rule.Word ?? "")));
            var result = proecessor.ProcessRange(from, to);
            return result;
        }

        [HttpPost]
        [Route("custom-full")]
        public IEnumerable<NumberRule> GetCustomFull([FromBody] IEnumerable<NumberRule> rules, [FromQuery] int from = 100, [FromQuery] int to = 1)
        {
            var proecessor = _rulesProcessorFactory.GetRulesProcessor(rules.Select(rule=> new RuleParser(rule.Number, rule.Word ?? "")));
            var counter = from;
            var result = proecessor.ProcessRange(from, to).Select(res => new NumberRule { Number = (from < to ? counter++ : counter--), Word = res });
            return result;
        }

    }
}
