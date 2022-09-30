using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace 网关test
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        new WebHostBuilder()
    //        .UseKestrel()
    //        .UseContentRoot(Directory.GetCurrentDirectory())
    //        .ConfigureAppConfiguration((hostingContext, config) =>
    //        {
    //            config
    //                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
    //                .AddJsonFile("appsettings.json", true, true)
    //                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
    //                .AddJsonFile("ocelot.json")
    //                .AddEnvironmentVariables();
    //        })
    //        .ConfigureServices(s =>
    //        {
    //            s.AddOcelot();
    //        })
    //        .ConfigureLogging((hostingContext, logging) =>
    //        {
    //            //add your logging
    //        })
    //        .UseIISIntegration()
    //        .Configure(app =>
    //        {
    //            app.UseOcelot().Wait();
    //        })
    //        .Build()
    //        .Run();
    //    }
    //}

    //https://ocelot.readthedocs.io/en/latest/features/routing.html
    //https://cloud.tencent.com/developer/article/1464439
    //https://www.cnblogs.com/xhznl/p/13305767.html
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(conf =>
            {
                conf.AddJsonFile("ocelot.json", false, true);
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}