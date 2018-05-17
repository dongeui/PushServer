using System;
using System.Collections.Generic;
using System.Linq;
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
        public string DataLength { get; set; }
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
                    byte[] headerBytes = header.Array;
                    byte[] customerIdBytes = new byte[9];
                    for (int i = 0; i < 9; i++)
                    {
                        customerIdBytes[i] = headerBytes[1+i];
                    }
                   

                    byte typeByte = header.Array[16];

                    byte[] reqTimeBytes = new byte[6];
                    Array.Copy(bodyBuffer, offset, reqTimeBytes, 0, 6);

                    byte[] resTimeBytes = new byte[6];
                    Array.Copy(bodyBuffer, offset+6, resTimeBytes, 0, 6);

                    byte[] dataBytes = new byte[length];
                    Array.Copy(bodyBuffer, offset+12, dataBytes, 0, length);

                    packet.Stx = this.Stx;
                    //packet.CustomerID = Encoding.Default.GetString()


                }
                catch (Exception e)
                {

                }
            }

            return packet;
        }
    }
}
