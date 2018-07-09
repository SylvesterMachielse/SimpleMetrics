using App.Metrics;
using Autofac;
using SimpleMetrics.Contracts;
using SimpleMetrics.Contracts.Measuring;
using PromMetrics.Measuring;
using PromMetrics.Measuring.Models;
using SimpleMetrics.Services;

namespace SimpleMetrics
{
    public class MetricsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IMetricsRoot>().AsSelf().SingleInstance();
            builder.RegisterType<AppMetricsCounterIncrementer>().As<IIncrementMetricsCounters>().SingleInstance();
            builder.RegisterType<AppMetricsGaugeSetter>().As<ISetMetricsGauges>().SingleInstance();
            builder.RegisterType<AppMetricsApdexTracker>().As<ITrackApdexActions>().SingleInstance();
            builder.RegisterGeneric(typeof(MeasureMetricsModelFactory<>)).AsSelf().SingleInstance();
            builder.RegisterType<MetricSnapshotProvider>().As<IProvideMetricSnapshots>().SingleInstance();
            builder.RegisterType<ApdexModelFactory>().As<ICreateMeasureApdexModels>().SingleInstance();
            builder.RegisterType<MetricNameCreator>().As<ICreateMetricNames>().SingleInstance();

            builder.Register(x => new MetricsBuilder().OutputMetrics.AsPrometheusPlainText().Build()).SingleInstance();
        }
    }
}