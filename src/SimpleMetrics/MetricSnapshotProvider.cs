using System.IO;
using System.Text;
using App.Metrics;
using SimpleMetrics.Contracts;

namespace SimpleMetrics
{
    public class MetricSnapshotProvider : IProvideMetricSnapshots
    {
        private readonly IMetricsRoot _metrics;

        public MetricSnapshotProvider(IMetricsRoot metrics)
        {
            _metrics = metrics;
        }

        public string Provide()
        {
            var snapshot = _metrics.Snapshot.Get();

            string result = "";

            foreach (var formatter in _metrics.OutputMetricsFormatters)
            {
                using (var stream = new MemoryStream())
                {
                    formatter.WriteAsync(stream, snapshot);

                    result = Encoding.ASCII.GetString(stream.ToArray());
                }
            }

            result = result.Replace("\r", string.Empty);

            return result;
        }
    }
}
