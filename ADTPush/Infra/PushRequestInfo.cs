using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class PushRequestInfo : RequestInfo<Packet>
    {
        public PushRequestInfo(string key, Packet body) : base(key, body)
        {
        }
    }
}
