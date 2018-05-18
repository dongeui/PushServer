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
        /// <summary>   
        /// Type 0:응답, 1:로그인 2:명령코드
        /// 0: 1(성공),0(실패)
        /// 1: Token(모바일UID)
        /// 2: 전송명령코드
        /// </summary>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
          
            byte[] dataBytes = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                dataBytes[i] = header[offset + 10 + i];
            }
            var dataLength = int.Parse(Encoding.Default.GetString(dataBytes));

            return dataLength;
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            Packet packet = new Packet();
            var reqPacket = packet.Parse(header, bodyBuffer, offset, length);

            return new PushRequestInfo("Command", reqPacket);
        }
    }
}
