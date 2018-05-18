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
    [StructLayout(LayoutKind.Sequential)]
    public class sssss
    {
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =1)]
        public byte name;
    }
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

                    byte[] reqTimeBytes = new byte[6];
                    Array.Copy(bodyBuffer, offset, reqTimeBytes, 0, 6);

                    byte[] resTimeBytes = new byte[6];
                    Array.Copy(bodyBuffer, offset + 6, resTimeBytes, 0, 6);

                    byte[] dataBytes = new byte[length];
                    Array.Copy(bodyBuffer, offset + 12, dataBytes, 0, length);
                   
                    packet.Stx = this.Stx;
                    packet.CustomerID = Encoding.Default.GetString(idbytes);
                    packet.Type = Convert.ToString(typeByte);
                    packet.DataLength = length;
                    packet.Req_time = Encoding.Default.GetString(reqTimeBytes);
                    packet.Res_time = null;
                    packet.Data = Encoding.Default.GetString(dataBytes);
                    packet.Etx = this.Etx;

                }
                catch (Exception e)
                {

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
            builder.Append(obj.Req_time);
            builder.Append(obj.Data);
            builder.Append(obj.Etx);

            byte[] aaa = Encoding.Default.GetBytes(builder.ToString());
            return aaa;
        }
    }
}
