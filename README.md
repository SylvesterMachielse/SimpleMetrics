# SimpleMetrics

## Goals
* Start working with metrics easily
* Ensure best practices are used

## What is it
* a wrapper around [App.Metrics](https://www.app-metrics.io). 
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

If you have a .net application you might get an error:

> Could not load file or assembly 'System.Runtime, Version=4.0.0.0, 

Resolve this by adding/editing your web.config

```xml
<dependentAssembly>
    <assemblyIdentity name="System.Runtime" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-4.1.2.0" newVersion="4.1.2.0" />
</dependentAssembly>
```

### Incrementing a counter
Inject:
```csharp
private readonly IIncrementMetricsCounters _metricsCounterIncrementer;
```

Create a model. This is were you might want to write some basic factories that handle things like setting the namespace or add some standard labels. Be sure to read the [best practices](https://prometheus.io/docs/practices/naming/)!

```csharp
public class UserClickedSomethingMetric : IMetricsModel
{
    public UserClickedSomethingMetric()
    {
        this.Tags = new Dictionary<string, string>();
    }

    public string Namespace => "MyApplication";
    public string ThingBeingMeasured => "myfeature_clicked_somebutton";
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
