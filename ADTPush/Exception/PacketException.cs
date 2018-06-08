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
        public PacketException(string msg, string e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(msg, e.ToString());
        }
        public PacketException(Packet packet, string msg, System.Exception e)
        {
            DBControl dbc = new DBControl();
            dbc.ErrorLog(packet.CustomerID,  msg, e.ToString(), packet.PhoneNum);
        }
        
       
    }
}
