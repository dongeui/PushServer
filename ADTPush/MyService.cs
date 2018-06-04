using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ServiceModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel.Description;
using ADTPush.Infra;

namespace ADTPush
{
    public partial class MyService : ServiceBase
    {
        const int Port = 48484;
        const int MaxConnect = 3000;

        private BootstrapBuilder builder;
        public static PushServer server;
        public const string Name = "ADTPUSH";

        public MyService()
        {
            ServiceName = Name;
            builder = new BootstrapBuilder();
        }

     
        protected override void OnStart(string[] args)
        {
            server = builder.Build(Port, MaxConnect);
            server.Start();
            //Program.Start(args);
        }

        protected override void OnStop()
        {
            server.Stop();
            //Program.Stop();
        }

    }

   
}
