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
        public string DataLength { get; set; }
        public string Req_time { get; set; }
        public string Res_time { get; set; }
        public string Data { get; set; }
        public string Etx = "E";
    
      

        public byte[] PacketBytes(Packet obj)
        {
            byte[] aaa = null;
            try
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
                aaa = Encoding.Default.GetBytes(builder.ToString());
            }
            catch(System.Exception e)
            {
                throw new PacketException(obj, "PacketBytes Parse Error", e);
            }
            return aaa;

        }
        public string PacketString(Packet obj)
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

            return builder.ToString();
        }
    }
}
