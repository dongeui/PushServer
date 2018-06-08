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
            int dataLength = 0;

            //var length1 = header[offset + 10];
            //var length2 = header[offset + 11];
            //var length3 = header[offset + 12];
            //var length4 = header[offset + 13];
            //var length5 = header[offset + 14];
            //if (length1 != 0 || length2 != 0 || length3 != 0 || length4 != 0 || length5 != 0)
            //{
            //    dataLength = int.Parse(length1.ToString()) + int.Parse(length2.ToString()) + int.Parse(length3.ToString()) + int.Parse(length4.ToString()) + int.Parse(length5.ToString());
            //};

            try
            {
                byte[] dataBytes = new byte[5];
                for (int i = 0; i < 5; i++)
                {
                    //stx + customerNO + phoneNum + temp = 37
                    dataBytes[i] = header[offset + 37 + i];
                }
                if (dataBytes[0] != 0 || dataBytes[1] != 0 || dataBytes[2] != 0 || dataBytes[3] != 0 || dataBytes[4] != 0)
                    dataLength = int.Parse(Encoding.Default.GetString(dataBytes));

            }
            catch (System.Exception e)
            {
                //throw new PacketException("Packet Data Length Check Error", e);
                dataLength = 0;
            }

            return dataLength;

        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            if (length == 0)
                return new PushRequestInfo("ExceptionCommand", null);

            PushRequestInfo requestInfo;
            Packet reqPacket;
            try
            {
                reqPacket = PacketParser.Parse(header, bodyBuffer, offset, length);
                if (reqPacket.CustomerID == null)
                {
                    requestInfo = new PushRequestInfo("ExceptionCommand", null);
                }
                else
                {
                    //DBControl dbc = new DBControl();
                    //string aaa = reqPacket.PacketString(reqPacket);
                    //dbc.ServerLog(reqPacket.CustomerID, reqPacket.Type, reqPacket.Req_time);
                }

            }
            catch (System.Exception e)
            {
                throw new PacketException("RequestInfo Error", e);
            }
            requestInfo = new PushRequestInfo("Command", reqPacket);
            return requestInfo;
        }
    }
}
