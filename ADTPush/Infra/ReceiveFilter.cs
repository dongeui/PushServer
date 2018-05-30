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
            //byte[] testbytes = new byte[16];
            //for(int i=0; i<length; i++){
            //    testbytes[i] = header[offset+i];
            //}
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
                byte[] newPacket = new byte[length];
                for(int i=0; i<length; i++){
                    newPacket[i] = header[offset+i];
                }
                throw new PacketException(newPacket, "Packet Data Length Check Error", e);
            }
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            PushRequestInfo requestInfo;
            //try
            //{
                var reqPacket = PacketParser.Parse(header, bodyBuffer, offset, length);
                if (reqPacket.CustomerID == null)
                {
                    requestInfo = new PushRequestInfo("ExceptionCommand", null);
                }
                else
                {
                    DBControl dbc = new DBControl();
                    dbc.ServerLog(reqPacket.CustomerID, reqPacket.Type, reqPacket.Req_time);
                }
                
            //}
            //catch (System.Exception e)
            //{
            //    throw new PacketException("RequestInfo Error", e);
            //}
            requestInfo = new PushRequestInfo("Command", reqPacket);
            return requestInfo;
        }
    }
}
