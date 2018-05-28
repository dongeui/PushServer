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
                switch (reqPacket.Type)
                {
                    case "49":
                        //db
                        dbc.RegisterInfo(reqPacket.CustomerID, reqPacket.Data);

                        Console.WriteLine("packet : : " + Encoding.Default.GetString(reqPacket.PacketBytes(reqPacket)));
                        //client response
                        pp.Type = "0";
                        string text = "로그인 응답";
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
                        {
                            //응답 로그
                            dbc.ServerLog(pp.CustomerID, pp.Type, DateTime.Parse(pp.Res_time), "True");
                            Console.WriteLine("Send 성공");
                        }
                        if (!resResult)
                        {
                            dbc.ServerLog(pp.CustomerID, pp.Type, DateTime.Parse(pp.Res_time), "False");
                            Console.WriteLine("Send 실패");
                        }

                        break;
                    case "50":
                        //db & msg send
                        string resultToken = dbc.SelectInfoById(reqPacket.CustomerID);
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
                                    mssg = MessageList.test;
                                    break;
                                default:
                                    break;
                            }

                            SendMessage msg = new SendMessage();
                            bool bobo = msg.Send(resultToken, mssg);

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
            catch (System.Exception e)
            {
                throw new PacketException(requestInfo.Body, "Command Error", e);
            }
        }
    }
}
