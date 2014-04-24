using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using log4net.Config;
using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;
using Nancy;
using Nancy.Owin;
using Owin;

namespace AzureWorkerNancySample.WorkerRole
{
    public class Program : RoleEntryPoint
    {
        private IDisposable _app;

        public override void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        public override bool OnStart()
        {
            XmlConfigurator.Configure();
            ServicePointManager.DefaultConnectionLimit = 12;

            
            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Http"];
            var baseUri = String.Format("{0}://{1}", endpoint.Protocol, endpoint.IPEndpoint);

            Trace.TraceInformation(String.Format("Starting OWIN at {0}", baseUri), "Information");
            _app = WebApp.Start<Startup>(new StartOptions(url: baseUri));

            return base.OnStart();
        }

        public override void OnStop()
        {
            _app.Dispose();
            base.OnStop();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new NancyOptions { });
        }
    }

    public class Mod : NancyModule
    {
        public Mod()
        {
            Get["/"] = x => "hi";
        }
    }
}
