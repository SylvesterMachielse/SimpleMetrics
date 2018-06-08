//using App.Metrics;
//using Metrics.Contracts;
//using Metrics.Contracts.Measuring;

//namespace Metrics.Counters
//{
//    internal class MetricsCounter : IIncrementMetricsCounters
//    {
//        private readonly IMetricsRoot _metrics;
//        private readonly ICreateCounterOptions _counterOptionsFactory;

//        public MetricsCounter(IMetricsRoot metrics, ICreateCounterOptions counterOptionsFactory)
//        {
//            _metrics = metrics;
//            _counterOptionsFactory = counterOptionsFactory;
//        }

//        public void Increment(IMetricsModel model)
//        {
//            var counterChangeModel = _counterOptionsFactory.Create(model);

//            _metrics.Measure.Counter.Increment(counterChangeModel.Options, counterChangeModel.Tags);
//        }
//    }
//}