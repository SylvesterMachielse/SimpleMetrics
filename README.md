# SimpleMetrics

## Goals
* Start working with metrics easily
* Ensure best practices are used

## What is it
This is a wrapper around [App.Metrics](https://www.app-metrics.io) that  is more suitable for our organisation: 
* Autofac for dependency injection
* can act as an extra layer in naming/managing metrics in your codebase
* formats as prometheus

## Configuration

### Adding SimpleMetrics to your project
``` 
Install-Package SimpleMetrics
```

In your autofac dependency registration
```csharp
builder.RegisterModule<MetricsModule>();
```

### Incrementing a counter
Inject:
```csharp
private readonly IIncrementMetricsCounters _metricsCounterIncrementer;
```

Create a model:

```csharp
public class UserClickedSomethingMetric : IMetricsModel
{
    public UserClickedSomethingMetric()
    {
        this.Tags = new Dictionary<string, string>();
    }

    public string Namespace => "MyApplication";
    public string ThingBeingMeasured => "findandreplace_clicked_find";
    public string Unit => string.Empty;
    public string Suffix => "total";
    public Dictionary<string, string> Tags { get; }
}
```

Increment the counter
```csharp
_metricsCounterIncrementer.Increment(new UserClickedFindMetric());
``` 

### Adding a metric endpoint for prometheus to scrape
Add a controller:

```csharp
[Route("metrics")]
public class MetricsController : Controller
{
    private readonly IProvideMetricSnapshots _metricsSnapshotProvider;

    public MetricsController(IProvideMetricSnapshots metricsSnapshotProvider)
    {
        _metricsSnapshotProvider = metricsSnapshotProvider;
    }

    [HttpGet]
    public ActionResult Metrics()
    {
        var snapshot = _metricsSnapshotProvider.Provide();

        return new ContentResult() {Content = snapshot, ContentEncoding = Encoding.UTF8, ContentType = "text/html"};
    }
}
```

If you use an ApiController
```csharp
[Route("metrics")]
[HttpGet]
public HttpResponseMessage Get()
{
    var snapshot = _metricSnapshotProvider.Provide();

    return new HttpResponseMessage()
    {
        Content = new StringContent(
            snapshot,
            Encoding.UTF8,
            "text/html"
        )
    };
}
```
