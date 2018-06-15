using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;

namespace SimpleMetricsTest.Controllers
{
    [Route("api/[controller]")]
    
    public class ValuesController : Controller
    {
        private readonly IIncrementMetricsCounters _metricsCounterIncrementer;

        public ValuesController(IIncrementMetricsCounters metricsCounterIncrementer)
        {
            _metricsCounterIncrementer = metricsCounterIncrementer;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _metricsCounterIncrementer.Increment(new ValuesModel());

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class ValuesModel : IMetricsModel
    {
        public ValuesModel()
        {
            this.Tags = new Dictionary<string, string>();
        }

        public string Namespace => "test_application";
        public string ThingBeingMeasured => "valuescontroller";
        public string Unit { get; }
        public string Suffix => "total";
        public Dictionary<string, string> Tags { get; }
    }
}
