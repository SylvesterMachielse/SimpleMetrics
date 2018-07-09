using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMetrics.Contracts;
using SimpleMetricsTest.Services;

namespace SimpleMetricsTest.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IProvideMetricSnapshots _metricSnapshotProvider;
        private readonly FakeMetricIncrementer _fakeMetricIncrementer;

        public MetricsController(IProvideMetricSnapshots metricSnapshotProvider, FakeMetricIncrementer fakeMetricIncrementer)
        {
            _metricSnapshotProvider = metricSnapshotProvider;
            _fakeMetricIncrementer = fakeMetricIncrementer;
        }

        [Route("metrics")]

        [HttpGet]
        public ActionResult Get()
        {
            _fakeMetricIncrementer.Increment();

            var snapshot = _metricSnapshotProvider.Provide();

            var result = new ContentResult()
            {
                Content = snapshot,
                ContentType = "text/application",
                StatusCode = StatusCodes.Status200OK
            };

            return result;
        }
    }
}