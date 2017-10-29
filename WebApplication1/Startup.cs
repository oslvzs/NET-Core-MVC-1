using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading;

namespace WebApplication1
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                //Do some work here
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                await context.Response.WriteAsync("App started.");
                Thread.Sleep(1000);
                await next();
                Thread.Sleep(1560);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
             
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                 await context.Response.WriteAsync("RunTime through lambda: " + elapsedTime +".");
            });

            app.UseMiddleware<StopwatchMiddleware>();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Test.");
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("End.");
            });
        }
    }
}
