using App.Metrics.Apdex;
using SimpleMetrics.Contracts;

namespace PromMetrics.Measuring.Models
{
    internal class ApdexModelFactory : ICreateMeasureApdexModels
    {
        private readonly MeasureMetricsModelFactory<ApdexOptions> _baseMetricFactory;

        public ApdexModelFactory(MeasureMetricsModelFactory<ApdexOptions> baseMetricFactory)
        {
            _baseMetricFactory = baseMetricFactory;
        }

        public MeasureMetricsModel<ApdexOptions> Create(IApdexModel model)
        {
            var result = _baseMetricFactory.Create(model);

            result.Options.ApdexTSeconds = model.ApdexTSeconds;

            return result;
        }
    }
}
