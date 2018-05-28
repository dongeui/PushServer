using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ADTPush
{
    public partial class MyService : ServiceBase
    {
        public const string Name = "ADTPUSH";
        public MyService()
        {
            ServiceName = Name;
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            Program.Start(args);
            //base.OnStart(args);
        }

        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
         
            base.OnStop();
        }

    }

   
}
