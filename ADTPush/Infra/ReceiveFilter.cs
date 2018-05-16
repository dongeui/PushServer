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
        /// <summary>
        /// Type 0:응답, 1:로그인 2:명령코드
        /// 0: 1(성공),0(실패)
        /// 1: Token(모바일UID)
        /// 2: 전송명령코드
        /// </summary>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            byte[] dataBytes = new byte[5];
            //잘받았으면 여기서 type만 1로 바꿔서 리스폰스(세션에다가)
            for(int i=0; i<5; i++)
            {
                dataBytes[i] = header[offset + 10 + i];
            }
            var dataLength = int.Parse(Encoding.Default.GetString(dataBytes));

            return dataLength;
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            //여기서 타입구분해서 로그인이면 token태우고 
            Packet packet = new Packet();
            var reqPacket = packet.Parse(header, bodyBuffer, offset, length);

            TokenControl tc = new TokenControl(reqPacket);
            tc.Connection();

            //타입이 명령코드면 샌드메세지
            SendMessage msg = new SendMessage();

            throw new NotImplementedException();
        }
    }
}
