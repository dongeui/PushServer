using ADTPush.Exception;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class ReceiveFilter : FixedHeaderReceiveFilter<PushRequestInfo>
    {
        public ReceiveFilter() : base(Packet.HeaderSize) { }
      
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            try
            {
                byte[] dataBytes = new byte[5];
                for (int i = 0; i < 5; i++)
                {
                    dataBytes[i] = header[offset + 10 + i];
                }
                var dataLength = int.Parse(Encoding.Default.GetString(dataBytes));
                return dataLength;
            }
            catch(System.Exception e)
            {
                Packet pp = new Packet();
                throw new PacketException(pp, "Packet Data Length Check Error", e);
            }
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            Packet packet = new Packet();
            var reqPacket = packet.Parse(header, bodyBuffer, offset, length);

            DBControl dbc = new DBControl();
            dbc.ServerLog(reqPacket.CustomerID, reqPacket.Type, DateTime.Parse(reqPacket.Req_time));

            return new PushRequestInfo("Command", reqPacket);
        }
    }
}
