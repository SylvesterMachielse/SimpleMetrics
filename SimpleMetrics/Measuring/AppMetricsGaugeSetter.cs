using App.Metrics;
using App.Metrics.Gauge;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;
using PromMetrics.Measuring.Models;

namespace PromMetrics.Measuring
{
    internal class AppMetricsGaugeSetter : ISetMetricsGauges
    {
        private readonly IMetricsRoot _metrics;
        private readonly MeasureMetricsModelFactory<GaugeOptions> _measureMetricsModelFactory;

        public AppMetricsGaugeSetter(IMetricsRoot metrics, MeasureMetricsModelFactory<GaugeOptions> measureMetricsModelFactory)
        {
            _metrics = metrics;
            _measureMetricsModelFactory = measureMetricsModelFactory;
        }

        public void Set(IMetricsModel model, double value)
        {
            var measureMetricsModel = _measureMetricsModelFactory.Create(model);

            if (measureMetricsModel.Tags.Count > 0)
            {
                _metrics.Measure.Gauge.SetValue(measureMetricsModel.Options, measureMetricsModel.Tags, value);
            }

            _metrics.Measure.Gauge.SetValue(measureMetricsModel.Options, value);
        }

        public void Set(IMetricsModel model, int value)
        {
            var measureMetricsModel = _measureMetricsModelFactory.Create(model);

            if (measureMetricsModel.Tags.Count > 0)
            {
                _metrics.Measure.Gauge.SetValue(measureMetricsModel.Options, measureMetricsModel.Tags, value);
            }

            _metrics.Measure.Gauge.SetValue(measureMetricsModel.Options, value);
        }
    }
}
