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
                switch (reqPacket.Type)
                {
                    case "49":
                        //db
                        dbc.RegisterInfo(reqPacket.CustomerID, reqPacket.Data);

                        //client response
                        pp.Type = "0";
                        string text = "Login Success";
                        pp.DataLength = text.Length;
                        pp.Data = text;
                        var ppBytes = pp.PacketBytes(pp);
                        resResult = session.TrySend(ppBytes, 0, ppBytes.Length);

                        if (resResult)
                        {
                            
                        }

                        break;
                    case "50":
                        //db & msg send
                        string resultToken = dbc.SelectInfoById(reqPacket.CustomerID);
                        if (resultToken != null)
                        {
                            var messageType = reqPacket.Data;
                            SendMessage msg = new SendMessage();
                            bool bobo = msg.Send(resultToken, MessageList.test);

                        }

                        //client response
                        pp.Type = "0";
                        string text2 = "Message Success";
                        pp.DataLength = text2.Length;
                        pp.Data = text2;
                        var ppBytes2 = pp.PacketBytes(pp);
                        resResult = session.TrySend(ppBytes2, 0, ppBytes2.Length);

                        break;
                    default:
                        //client repsonse
                        pp.Type = "0";
                        string text3 = "ERROR";
                        pp.DataLength = text3.Length;
                        pp.Data = text3;
                        var ppBytes3 = pp.PacketBytes(pp);
                        resResult = session.TrySend(ppBytes3, 0, ppBytes3.Length);

                        break;
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}
