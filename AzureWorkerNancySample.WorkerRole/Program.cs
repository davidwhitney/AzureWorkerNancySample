using System.Diagnostics;
using System.Net;
using System.Threading;
using log4net.Config;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AzureWorkerNancySample.WorkerRole
{
    public class Program : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("AzureWorkerNancySample.WorkerRole entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            XmlConfigurator.Configure();
            
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
