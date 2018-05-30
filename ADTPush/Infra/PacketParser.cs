using ADTPush.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public static class PacketParser
    {
        public static Packet Parse(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
                try
                {
                    var packet = new Packet();
                    byte[] idbytes = new byte[9];
                    for (int i = 0; i < 9; i++)
                    {
                        idbytes[i] = header.Array[i + 1];
                    }

                    byte typeByte = header.Array[15];

                    int timeSize = 14;
                    byte[] reqTimeBytes = new byte[timeSize];
                    Array.Copy(bodyBuffer, offset, reqTimeBytes, 0, timeSize);

                    byte[] resTimeBytes = new byte[timeSize];
                    Array.Copy(bodyBuffer, offset + timeSize, resTimeBytes, 0, timeSize);

                    byte[] dataBytes = new byte[length];
                    Array.Copy(bodyBuffer, offset + timeSize + timeSize, dataBytes, 0, length);

                    packet.Stx = "S";
                    packet.CustomerID = Encoding.Default.GetString(idbytes);
                    packet.Type = Convert.ToString(typeByte);
                    packet.DataLength = length.ToString().PadLeft(5,'0');
               
                    var dataLength = int.Parse(Encoding.Default.GetString(dataBytes));
                    packet.Req_time = Encoding.Default.GetString(reqTimeBytes);
                    packet.Res_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    packet.Data = Encoding.Default.GetString(dataBytes);
                    packet.Etx = "E";
                    return packet;
                }
                catch (System.Exception e)
                {
                    throw new PacketException("Packet Parse Error", e);
                }

         
        }
    }
}
