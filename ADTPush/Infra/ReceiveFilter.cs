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
        /// 0: Module, 1:App
        /// </summary>
        public int InputType;
        
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            TokenControl tc = new TokenControl();
            tc.Connection();
            //여기서 토큰, 계약번호 둘다있으면 앱, 계약번호만있으면 모듈로부터온것
            //InputType수정하고 바디랭스리턴
            InputType = int.Parse(header[offset].ToString());
            var CustomIdLength = header[offset + 1].ToString();
            if (InputType == 0)
            {

            }

            throw new NotImplementedException();
        }

        protected override PushRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            //InputType에 따라서 확인
            //body length가지고 보내기
            //sendmessage호출사용 딴데로보내지말고
            //여기까지이벤트됨
            throw new NotImplementedException();
        }
    }
}
