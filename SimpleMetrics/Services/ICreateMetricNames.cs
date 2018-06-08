namespace SimpleMetrics.Services
{
    interface ICreateMetricNames
    {
        string Build(string thingBeingMeasured, string unit, string suffix);
    }
}