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
        public int Type = 5;
        public ReceiveFilter() : base(Packet.HeaderSize) { }
        /// <summary>   
        /// Type 0:응답, 1:로그인 2:명령코드
        /// 0: 1(성공),0(실패)
        /// 1: Token(모바일UID)
        /// 2: 전송명령코드
        /// </summary>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            Type = int.Parse(header[15].ToString());
            if (Type == 5)
            {
                
            }
            else
            {
                //잘받았다고 리스폰스
            }
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
            /*
             * 1 : Id, Token 저장 
             * 2 : 
             *    2-1) Id != Null
             *         Token 비교 후 같으면 단순 전송, 다르면 Token Update
             *    2-2) Id == Null 
             *         Error Response
             */
            Packet packet = new Packet();
            var reqPacket = packet.Parse(header, bodyBuffer, offset, length);
            DBControl dbc = new DBControl();

            if (Type == 1)
            {
                dbc.RegisterInfo(reqPacket.CustomerID, reqPacket.Data);
            }
            else if (Type == 2)
            {
                //여기서 select보내서 id랑 같은게 나왔을때 다시 비교함

                string resultToken = dbc.SelectInfoById(reqPacket.CustomerID);

                //bool result = dbc.CheckInfo(reqPacket.CustomerID);
                if (resultToken!=null)
                {
                    SendMessage msg = new SendMessage();
                    //여기서 data는 messageType , reqpacket.data랑 비교해서 같은거 넣어줌
                    
                    msg.Send(resultToken, MessageList.test1);
                }
            }




            //SendMessage msg = new SendMessage();

            throw new NotImplementedException();
        }
    }
}
