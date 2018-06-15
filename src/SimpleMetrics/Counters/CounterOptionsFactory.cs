using App.Metrics.Counter;
using SimpleMetrics.Contracts;
using PromMetrics.Measuring.Models;

namespace PromMetrics.Counters
{
    internal class CounterOptionsFactory : ICreateCounterOptions
    {
        private readonly MeasureMetricsModelFactory<CounterOptions> _baseMetricFactory;

        public CounterOptionsFactory(MeasureMetricsModelFactory<CounterOptions> baseMetricFactory)
        {
            _baseMetricFactory = baseMetricFactory;
        }

        public MeasureMetricsModel<CounterOptions> Create(IMetricsModel model)
        {
            var result = _baseMetricFactory.Create(model);

            return result;
        }
    }
}