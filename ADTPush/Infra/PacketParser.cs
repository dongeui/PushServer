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
            string ee = null;
            try
            {
                var packet = new Packet();
                byte[] idbytes = new byte[9];
                for (int i = 0; i < 9; i++)
                {
                    idbytes[i] = header.Array[i + 1];
                }

                byte[] phoneNumBytes = new byte[11];
                for (int i = 0; i < 11; i++)
                {
                    phoneNumBytes[i] = header.Array[i + 10];
                }
                
                byte[] tempBytes = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    tempBytes[i] = header.Array[i + 21];
                }

                byte typeByte = header.Array[42];

                int timeSize = 14;
                byte[] reqTimeBytes = new byte[timeSize];
                Array.Copy(bodyBuffer, offset, reqTimeBytes, 0, timeSize);

                byte[] resTimeBytes = new byte[timeSize];
                Array.Copy(bodyBuffer, offset + timeSize, resTimeBytes, 0, timeSize);

                byte osBytes = new byte();
                osBytes = bodyBuffer[offset + timeSize + timeSize];

                byte[] dataBytes = new byte[length];
                Array.Copy(bodyBuffer, offset + timeSize + timeSize + 1, dataBytes, 0, length-1);

                packet.Stx = "S";
                packet.PhoneNum = Encoding.Default.GetString(phoneNumBytes);
                packet.Temp = Encoding.Default.GetString(tempBytes);
                packet.CustomerID = Encoding.Default.GetString(idbytes);
                packet.Type = Convert.ToString(typeByte);
                packet.DataLength = length.ToString().PadLeft(5, '0');
                packet.OS = Convert.ToString(osBytes);

                //var dataLength = int.Parse(Encoding.Default.GetString(dataBytes));
                packet.Req_time = Encoding.Default.GetString(reqTimeBytes);
                packet.Res_time = Encoding.Default.GetString(resTimeBytes);
                //packet.Res_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                packet.Data = Encoding.Default.GetString(dataBytes);
                packet.Etx = "E";

                ee = packet.PacketString(packet);

                return packet;
            }
            catch (System.Exception e)
            {
                throw new PacketException("Packet Parse Error", ee);
            }


        }
    }
}
