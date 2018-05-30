using ADTPush.Infra;
using System;
using System.IO;
using System.ServiceProcess;

namespace ADTPush
{

     public static class Program
    {

        const int Port = 48484;
        const int MaxConnect = 3000;
        public static PushServer server;
        static void Main(string[] args)
        {
            Start(args);
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //     new MyService()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
        public static void Start(string[] args)
        {
            var builder = new BootstrapBuilder();
            server = builder.Build(Port, MaxConnect);
            //server.Start();


            if (!server.Start())
            {
                Console.WriteLine("Failed to start server");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine(server.Name + " Server,  Status : " + server.State + ",  Start Time : " + server.StartedTime);
            }
            Console.WriteLine("Press ' q ' to shutdown the server.");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

        }

        public static void Stop()
        {
            server.Stop();
        }
    }
}
