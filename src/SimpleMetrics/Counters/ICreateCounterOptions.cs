using App.Metrics.Counter;
using SimpleMetrics.Contracts;
using PromMetrics.Measuring.Models;

namespace PromMetrics.Counters
{
    internal interface ICreateCounterOptions
    {
        MeasureMetricsModel<CounterOptions> Create(IMetricsModel model);
    }
}