using System;
using App.Metrics;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;
using PromMetrics.Measuring.Models;

namespace PromMetrics.Measuring
{
    internal class AppMetricsApdexTracker : ITrackApdexActions
    {
        private readonly IMetricsRoot _metrics;
        private readonly ICreateMeasureApdexModels _measureApdexModelFactory;
        public AppMetricsApdexTracker(IMetricsRoot metrics, ICreateMeasureApdexModels measureApdexModelFactory)
        {
            _metrics = metrics;
            _measureApdexModelFactory = measureApdexModelFactory;
        }

        public IDisposable Track(IApdexModel model)
        {
            var measureMetricsModel = _measureApdexModelFactory.Create(model);

            if (measureMetricsModel.Tags.Count > 0)
            {
                return _metrics.Measure.Apdex.Track(measureMetricsModel.Options, measureMetricsModel.Tags);
            }

            return _metrics.Measure.Apdex.Track(measureMetricsModel.Options);
        }
    }
}
