using App.Metrics;
using App.Metrics.Counter;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;
using PromMetrics.Measuring.Models;

namespace PromMetrics.Measuring
{
    internal class AppMetricsCounterIncrementer : IIncrementMetricsCounters
    {
        private readonly MeasureMetricsModelFactory<CounterOptions> _measureMetricsModelFactory;
        private readonly IMetricsRoot _metrics;

        public AppMetricsCounterIncrementer(MeasureMetricsModelFactory<CounterOptions> measureMetricsModelFactory, IMetricsRoot metrics)
        {
            _measureMetricsModelFactory = measureMetricsModelFactory;
            _metrics = metrics;
        }

        public void Increment(IMetricsModel model)
        {
            var measureMetricsModel = _measureMetricsModelFactory.Create(model);

            if (measureMetricsModel.Tags.Count > 0)
            {
                _metrics.Measure.Counter.Increment(measureMetricsModel.Options, measureMetricsModel.Tags);
            }

            _metrics.Measure.Counter.Increment(measureMetricsModel.Options);
        }
    }
}
