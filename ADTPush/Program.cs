using ADTPush.Infra;
using System;
using System.IO;
using System.ServiceProcess;
using System.ServiceModel;
using System.ServiceModel.Description;
using SuperSocket.Common;
using System.Reflection;

namespace ADTPush
{

     public static class Program 
    {
       
     
        static void Main(string[] args)
        {

            //콘솔로 시작 + 속성 콘솔응용프로그램
            //Start(args);


            //서비스로 시작 + 속성 윈도우프로그램
            if (!Environment.UserInteractive)//Windows Service
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                 new MyService(),
                };
                ServiceBase.Run(ServicesToRun);

                return;
            }
          
        }

        public static void Start(string[] args)
        {

            //var builder = new BootstrapBuilder();
            //server = builder.Build(Port, MaxConnect);
            //서비스 시작은 단순 이걸로
            //server.Start();

            //서비스로 시작하면 console명령어 다빼주기
            //if (!server.Start())
            //{
            //    Console.WriteLine("Failed to start server");
            //    Console.ReadKey();
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine(server.Name + " Server,  Status : " + server.State + ",  Start Time : " + server.StartedTime);
            //}
            //Console.WriteLine("Press ' q ' to shutdown the server.");

            //while (Console.ReadKey().KeyChar != 'q')
            //{
            //    Console.WriteLine();
            //    continue;
            //}

        }
        

        public static void Stop()
        {
        }
    }
}
