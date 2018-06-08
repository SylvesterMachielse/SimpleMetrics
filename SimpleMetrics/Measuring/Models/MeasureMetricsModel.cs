using App.Metrics;

namespace PromMetrics.Measuring.Models
{
    internal class MeasureMetricsModel<T> where T : MetricValueOptionsBase
    {
        public T Options { get; set; }
        public MetricTags Tags { get; set; }
    }
}
