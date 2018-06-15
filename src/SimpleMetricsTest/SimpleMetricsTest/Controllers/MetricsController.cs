using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SimpleMetrics.Contracts;

namespace SimpleMetricsTest.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IProvideMetricSnapshots _metricSnapshotProvider;

        public MetricsController(IProvideMetricSnapshots metricSnapshotProvider)
        {
            _metricSnapshotProvider = metricSnapshotProvider;
        }

        [Route("metrics")]
        [HttpGet]
        public string Get()
        {
            var snapshot = _metricSnapshotProvider.Provide();

            return snapshot;
        }
    }
}