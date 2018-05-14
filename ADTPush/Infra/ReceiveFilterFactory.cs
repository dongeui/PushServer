using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ADTPush.Infra;

namespace ADTPush.Infra
{
    public class ReceiveFilterFactory : DefaultReceiveFilterFactory<ReceiveFilter, PushRequestInfo>
    {
        public override IReceiveFilter<PushRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            var filter = new ReceiveFilter
            {
            };
            return filter;
        }
    }
}
