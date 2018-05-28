using ADTPush.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Exception
{
    public class PacketException : System.Exception
    {
        public PacketException() { }
        public PacketException(Packet packet, string msg, System.Exception e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(packet.CustomerID, packet.Req_time, msg, e.ToString());

        }


    }
}
