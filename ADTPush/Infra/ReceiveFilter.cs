using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class ReceiveFilter : FixedHeaderReceiveFilter<PushRequestInfo>
    {
        public ReceiveFilter() : base(Packet.HeaderSize) { }
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //return body length
            throw new NotImplementedException();
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            //body length가지고 보내기
            //sendmessage호출사용 딴데로보내지말고
            //여기까지이벤트됨
            throw new NotImplementedException();
        }
    }
}
