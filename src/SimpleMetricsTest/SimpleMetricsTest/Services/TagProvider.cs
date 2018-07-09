using System;
using System.Collections.Generic;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;

namespace SimpleMetricsTest.Services
{
    public class FakeMetricIncrementer
    {
        private readonly IIncrementMetricsCounters _metricsCounterIncrementer;
        private List<MetricModel> _fakeMetrics;
        private Int64 _totalMetrics = 0;
        private readonly Random _random = new System.Random(DateTime.Now.Millisecond);

        public FakeMetricIncrementer(FakeMetricModelProvider _fakeMetricModelProvider, IIncrementMetricsCounters metricsCounterIncrementer )
        {
            _fakeMetrics = _fakeMetricModelProvider.Provide();
            _metricsCounterIncrementer = metricsCounterIncrementer;
        }

        public void Increment()
        {
            foreach (var fakeMetric in _fakeMetrics)
            {
                //var increment = _random.Next(0, 100);
                var increment = 1;

                for (int i = 0; i < increment; i++)
                {
                    _metricsCounterIncrementer.Increment(fakeMetric);
                }
            }

            _totalMetrics = _totalMetrics + _fakeMetrics.Count;

            Console.WriteLine($"{_totalMetrics} fake metrics...");
        }
    }

    public class FakeThingBeingMeasured
    {
        public FakeThingBeingMeasured(string space, string name, FakeTag possibleTags)
        {
            Namespace = space;
            Name = name;
            PossibleTags = possibleTags;
        }

        public string Namespace { get; set; }
        public string Name { get; set; }
        public FakeTag PossibleTags { get; set; }
        public string Unit { get; set; }
        public string Suffix { get; set; }
    }

    public class FakeThingsBeingMeasuredProvider
    {
        private readonly string[] namespaces = new[] {"web", "sql", "studio"};

        //private readonly string[] thingsBeingMeasured = new[] {"request", "logins", "overview_views", "detailview_views", "find", "replace", "preview","restore","synchronization","reporting"};
        private readonly string[] thingsBeingMeasured = new[] { "request" };

        private readonly int duplicator = 1;

        private readonly TagProvider _tagProvider;

        public FakeThingsBeingMeasuredProvider(TagProvider tagProvider)
        {
            _tagProvider = tagProvider;
        }

        public List<FakeThingBeingMeasured> Provide()
        {
            var results = new List<FakeThingBeingMeasured>();

            foreach (var name in namespaces)
            {
                foreach (var thing in thingsBeingMeasured)
                {
                    foreach (var tagSet in _tagProvider.Provide())
                    {
                        for (int i = 0; i < duplicator; i++)
                        {
                            results.Add(new FakeThingBeingMeasured(name, $"{thing}_{i}", tagSet));
                        }
                    }
                }
            }

            return results;
        }
    }

    public class FakeMetricModelProvider
    {
        private List<FakeThingBeingMeasured> _fakeThingsBeingMeasured;

        public FakeMetricModelProvider(FakeThingsBeingMeasuredProvider fakeThingsBeingMeasuredProvider)
        {
            _fakeThingsBeingMeasured = fakeThingsBeingMeasuredProvider.Provide();
        }

        public List<MetricModel> Provide()
        {
            var result = new List<MetricModel>();

            foreach (var fakeThingBeingMeasured in _fakeThingsBeingMeasured)
            {
                foreach (string possibleTag in fakeThingBeingMeasured.PossibleTags.PossibleValues)
                {
                    var metric = new MetricModel();
                    metric.Namespace = fakeThingBeingMeasured.Name;
                   
                    result.Add( new MetricModel()
                    {
                        Namespace = fakeThingBeingMeasured.Namespace,
                        ThingBeingMeasured = fakeThingBeingMeasured.Name,
                        Tags = new Dictionary<string, string>() { { fakeThingBeingMeasured.PossibleTags.Name, possibleTag} },
                        Suffix = "total",
                        //Unit = fakeThingBeingMeasured.Unit

                    });
                }
            }

            return result;
        }
    }

    public class MetricModel : IMetricsModel
    {
        public string Namespace { get; set; }
        public string ThingBeingMeasured { get; set; }
        public string Unit { get; set; }
        public string Suffix { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }

    public class FakeTag
    {
        public FakeTag(string name, string[] possibleTags)
        {
            this.Name = name;
            this.PossibleValues = possibleTags;
        }

        public string Name { get; set; }
        public string[] PossibleValues { get; set; }
    }

    public class TagProvider
    {
        public List<FakeTag> Provide()
        {
            var results = new List<FakeTag>();

            results.Add(new FakeTag("METHOD",new []{"GET","PUT","DELETE","PATCH"} ));
            results.Add(new FakeTag("ACTION",new []{"USER_LOGIN","USER_LOGOUT"} ));
            results.Add(new FakeTag("FILETYPE",new []{"pdf","doc","msg","xslx","unknown"} ));
          

            return results;
        }



        //public static List<string, string> MethodTags = new KeyValuePair<string, string>()
        //{
        //    {"METHOD", "GET"},
        //    {"METHOD", "PUT"},
        //    {"METHOD", "PATCH"},
        //    {"METHOD", "DELETE"}
        //};

        //public static KeyValuePair<string, string> ActionTags = new KeyValuePair<string, string>()
        //{
        //    {"ACTION", "Action1"},
        //    {"ACTION", "Action2"},
        //    {"ACTION", "Action3"},
        //    {"ACTION", "Action4"}
        //};
    }
}