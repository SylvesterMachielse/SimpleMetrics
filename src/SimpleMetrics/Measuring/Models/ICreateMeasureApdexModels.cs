using App.Metrics.Apdex;
using SimpleMetrics.Contracts;

namespace PromMetrics.Measuring.Models
{
    internal interface ICreateMeasureApdexModels
    {
        MeasureMetricsModel<ApdexOptions> Create(IApdexModel model);
    }
}
