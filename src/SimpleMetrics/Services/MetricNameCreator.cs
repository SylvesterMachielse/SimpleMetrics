namespace SimpleMetrics.Services
{
    public class MetricNameCreator : ICreateMetricNames
    {
        public string Build(string thingBeingMeasured, string unit, string suffix)
        {
            if (unit == string.Empty)
            {
                return BuildWithoutUnit(thingBeingMeasured, suffix);
            }

            return BuildWithUnit(thingBeingMeasured, unit, suffix);
        }

        private string BuildWithUnit(string thingBeingMeasured, string unit, string suffix)
        {
            return $"{thingBeingMeasured}_{unit}_{suffix}";
        }

        private string BuildWithoutUnit(string thingBeingMeasured, string suffix)
        {
            return $"{thingBeingMeasured}_{suffix}";
        }
    }
}