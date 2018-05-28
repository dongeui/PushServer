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
                var pp = reqPacket;
                pp.Type = "0";

                switch (reqPacket.Type)
                {
                    //모바일 UID 등록
                    case "49":
                        dbc.RegisterInfo(reqPacket.CustomerID, reqPacket.Data);

                        string text = "0";
                        pp.DataLength = text.Length;
                        pp.Data = text;

                        try
                        {
                            var ppBytes = pp.PacketBytes(pp);
                            resResult = session.TrySend(ppBytes, 0, ppBytes.Length);

                        }
                        catch(System.Exception e)
                        {
                            throw new PacketException(reqPacket, "Packet Parse Error", e);
                        }

                        if (resResult)
                            dbc.ServerLog(pp.CustomerID, pp.Type, DateTime.Parse(pp.Res_time), "True");
                        if (!resResult)
                            dbc.ServerLog(pp.CustomerID, pp.Type, DateTime.Parse(pp.Res_time), "False");

                        break;

                    //메시지 전송
                    case "50":
                        string resultToken = dbc.SelectInfoById(reqPacket.CustomerID);

                        if (resultToken != null)
                        {
                            string te = "0";
                            pp.DataLength = te.Length;
                            pp.Data = te;

                            var ppBytes = pp.PacketBytes(pp);
                            resResult = session.TrySend(ppBytes, 0, ppBytes.Length);

                            string mssg = string.Empty;
                            var messageType = reqPacket.Data;
                            switch (messageType)
                            {
                                case "0":
                                    mssg = MessageList.test;
                                    break;
                                case "1":
                                    mssg = MessageList.test;
                                    break;
                                default:
                                    break;
                            }

                            //메세지 보내라고 테스트 하는 기능
                            SendMessage msg = new SendMessage();
                            bool bobo = msg.Send(resultToken, mssg);

                        }
                        if(resultToken == null) {
                            string te = "1";
                            pp.DataLength = te.Length;
                            pp.Data = te;
                            var ppBytes = pp.PacketBytes(pp);
                            resResult = session.TrySend(ppBytes, 0, ppBytes.Length);
                        }
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
