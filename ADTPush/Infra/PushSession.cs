using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class PushSession : AppSession<PushSession, PushRequestInfo>
    {
        public Context context;
        protected override void OnSessionStarted()
        {
            context = new Context(this);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
        }
    }
}
