using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class Context
    {
        public PushSession Session { get; set; }
        public Context(PushSession value)
        {
            Session = value;
        }
    }
}
