using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMetrics;
using SimpleMetricsTest.Services;

namespace SimpleMetricsTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<MetricsModule>();
            builder.RegisterType<FakeMetricIncrementer>().AsSelf().SingleInstance();
            builder.RegisterType<FakeThingsBeingMeasuredProvider>().AsSelf().SingleInstance();
            builder.RegisterType<FakeMetricModelProvider>().AsSelf().SingleInstance();
            builder.RegisterType<TagProvider>().AsSelf().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOwin();
            app.UseMvc();
        }
    }
}