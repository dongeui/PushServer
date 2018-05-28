using ADTPush.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
   
    public class Packet
    {
        public const int HeaderSize = 16;
        public string Stx = "S";
        public string CustomerID { get; set; }
        public string Type { get; set; }
        public int DataLength { get; set; }
        public string Req_time { get; set; }
        public string Res_time { get; set; }
        public string Data { get; set; }
        public string Etx = "E";
    


        public Packet Parse(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var packet = new Packet();
            if (packet != null)
            {
                try
                {
                    byte[] idbytes = new byte[9];
                    for (int i = 0; i < 9; i++)
                    {
                        idbytes[i] = header.Array[i + 1];
                    }

                    byte typeByte = header.Array[15];

                    int timeSize = 14;
                    byte[] reqTimeBytes = new byte[timeSize];
                    Array.Copy(bodyBuffer, offset, reqTimeBytes, 0, reqTimeBytes.Length);

                    byte[] resTimeBytes = new byte[timeSize];
                    Array.Copy(bodyBuffer, offset + reqTimeBytes.Length, resTimeBytes, 0, reqTimeBytes.Length);

                    byte[] dataBytes = new byte[length];
                    Array.Copy(bodyBuffer, offset + reqTimeBytes.Length + resTimeBytes.Length, dataBytes, 0, length);
                   
                    packet.Stx = this.Stx;
                    packet.CustomerID = Encoding.Default.GetString(idbytes);
                    packet.Type = Convert.ToString(typeByte);
                    packet.DataLength = length;
                    packet.Req_time = Encoding.Default.GetString(reqTimeBytes);
                    packet.Res_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    packet.Data = Encoding.Default.GetString(dataBytes);
                    packet.Etx = this.Etx;

                }
                catch (System.Exception e)
                {
                    throw new PacketException(packet, "Packet Parse Error", e);
                }
            }

            return packet;
        }

        public byte[] PacketBytes(Packet obj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(obj.Stx);
            builder.Append(obj.CustomerID);
            builder.Append(obj.DataLength);
            builder.Append(obj.Type);
            builder.Append(obj.Req_time);
            builder.Append(obj.Res_time);
            builder.Append(obj.Data);
            builder.Append(obj.Etx);

            byte[] aaa = Encoding.Default.GetBytes(builder.ToString());
            return aaa;
        }
    }
}
