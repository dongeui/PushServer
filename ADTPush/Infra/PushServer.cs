using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class PushServer : AppServer<PushSession, PushRequestInfo>
    {
        public PushServer() : base(new ReceiveFilterFactory())
        {

        }
    }
}
