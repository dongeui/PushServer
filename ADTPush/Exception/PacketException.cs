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
        
       
        public PacketException(string msg, System.Exception e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(msg, e.ToString());
        }
        public PacketException(Packet packet, string msg, System.Exception e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(packet.CustomerID, packet.Req_time, msg, e.ToString());
        }
        public PacketException(byte[] bytes, string msg, System.Exception e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(Convert.ToString(bytes), DateTime.Now.ToString(), msg, e.ToString());
        }
        
       
    }
}
