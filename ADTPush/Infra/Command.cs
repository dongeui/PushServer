using ADTPush.Exception;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class Command : CommandBase<PushSession, PushRequestInfo>
    {
        public override void ExecuteCommand(PushSession session, PushRequestInfo requestInfo)
        {
            try
            {
                DBControl dbc = new DBControl();
                bool resResult = false;
                var reqPacket = requestInfo.Body;
                var resPacket = new Packet
                {
                    Stx = reqPacket.Stx,
                    CustomerID = reqPacket.CustomerID,
                    PhoneNum = reqPacket.PhoneNum,
                    Temp = reqPacket.Temp,
                    Req_time = reqPacket.Req_time,
                    Res_time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Etx = reqPacket.Etx
                };

                switch (requestInfo.Body.Type)
                {
                    //모바일 UID 등록
                    case "49":
                        dbc.RegisterInfo(reqPacket.CustomerID, reqPacket.Data, reqPacket.PhoneNum, reqPacket.OS);

                        //1성공 0실패
                        string text = "1";
                        resPacket.Type = "0";
                        resPacket.DataLength = text.Length.ToString().PadLeft(5, '0');
                        resPacket.Data = text;

                        try
                        {
                            var resPacketBytes = resPacket.PacketBytes(resPacket);
                            resResult = session.TrySend(resPacketBytes, 0, resPacketBytes.Length);
                        }
                        catch (System.Exception e)
                        {
                            throw new PacketException(reqPacket, "Packet Send Error", e);
                        }

                        dbc.ServerLog(resPacket.CustomerID, reqPacket.Type, resPacket.Res_time, resResult.ToString(), reqPacket.PhoneNum);

                        break;

                    //메시지 전송
                    //phoneNumber가 있으면 해당하는 레지스터 토큰에만 보내면되고
                    //fffffffffff면 해당하는 계약번호에 해당하는 모든사람에게 보냄
                    case "50":
                        string resultToken = dbc.SelectTokenInfoById(reqPacket.CustomerID);
                        //49 AND, 50 IOS
                        string osType = dbc.SelectOSTypeById(reqPacket.CustomerID);

                        if (resultToken != null)
                        {
                            string mssg = string.Empty;
                            var messageType = reqPacket.Data;
                            switch (messageType)
                            {
                                case "0":
                                    mssg = MessageList.test;
                                    break;
                                case "1":
                                    mssg = MessageList.test1;
                                    break;
                                default:
                                    break;
                            }

                            SendMessage msg = new SendMessage();
                            bool bobo = true;
                            //bobo = msg.Send(resultToken, mssg);


                            if (bobo)
                            {
                                //send 성공
                                string te = "1";
                                resPacket.DataLength = te.Length.ToString().PadLeft(5, '0');
                                resPacket.Data = te;
                            }
                            if (!bobo)
                            {
                                //send 실패
                                string te = "0";
                                resPacket.DataLength = te.Length.ToString().PadLeft(5, '0');
                                resPacket.Data = te;
                            }

                            resPacket.Type = "0";

                            var resPacketBytes = resPacket.PacketBytes(resPacket);
                            resResult = session.TrySend(resPacketBytes, 0, resPacketBytes.Length);
                        }

                        if (resultToken == null)
                        {
                            string te = "0";
                            resPacket.DataLength = te.Length.ToString().PadLeft(5, '0');
                            resPacket.Data = te;
                            var resPacketBytes = resPacket.PacketBytes(resPacket);
                            resResult = session.TrySend(resPacketBytes, 0, resPacketBytes.Length);
                        }

                        dbc.ServerLog(resPacket.CustomerID, reqPacket.Type, resPacket.Res_time, resResult.ToString(), reqPacket.PhoneNum);

                        break;

                    default:
                        break;
                }

            }
            catch (System.Exception e)
            {
                throw new PacketException(requestInfo.Body, "Command Error", e);
            }
        }
    }
}
