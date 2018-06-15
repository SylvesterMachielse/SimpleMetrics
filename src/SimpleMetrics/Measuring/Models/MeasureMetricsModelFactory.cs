using System.Linq;
using App.Metrics;
using SimpleMetrics.Contracts;
using SimpleMetrics.Services;

namespace PromMetrics.Measuring.Models
{
    internal class MeasureMetricsModelFactory<T> where T : MetricValueOptionsBase, new()
    {
        private readonly ICreateMetricNames _metricNameCreator;

        public MeasureMetricsModelFactory(ICreateMetricNames metricNameCreator)
        {
            _metricNameCreator = metricNameCreator;
        }

        public MeasureMetricsModel<T> Create(IMetricsModel model)
        {
            var result = new MeasureMetricsModel<T>
            {
                Options = new T
                {
                    Name = _metricNameCreator.Build(model.ThingBeingMeasured, model.Unit, model.Suffix),
                    Context = model.Namespace
                },
                Tags = new MetricTags(model.Tags.Keys.ToArray(), model.Tags.Values.ToArray())
            };

            return result;
        }
    }
}
