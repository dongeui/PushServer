using ADTPush.Infra;
using System;
using System.IO;
using System.ServiceProcess;

namespace ADTPush
{

     public static class Program
    {

        public const string ServiceName = "ADTPUSH";
     
      
        const int Port = 48484;
        const int MaxConnect = 200;

        static void Main(string[] args)
        {

            Start(args);

        }
        public static void Start(string[] args)
        {

            var builder = new BootstrapBuilder();
            var server = builder.Build(Port, MaxConnect);


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

            server.Start();
        }

        public static void Stop()
        {
            
            //server.Stop();
        }
    }
}
