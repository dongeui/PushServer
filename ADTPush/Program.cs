using ADTPush.Infra;
using System;
using System.IO;
using System.ServiceProcess;

namespace ADTPush
{

     public static class Program
    {

        const int Port = 48484;
        const int MaxConnect = 200;
        public static PushServer server;
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                 new MyService()
            };
            ServiceBase.Run(ServicesToRun);
        }
        public static void Start(string[] args)
        {
            var builder = new BootstrapBuilder();
            server = builder.Build(Port, MaxConnect);
            server.Start();

        }

        public static void Stop()
        {
            server.Stop();
        }
    }
}
