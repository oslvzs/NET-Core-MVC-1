using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Do some work here
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await context.Response.WriteAsync("App started.");
            Thread.Sleep(800);
            await _next(context);
            Thread.Sleep(930);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            await context.Response.WriteAsync("RunTime through class: " + elapsedTime +".");
        }
    }
}

