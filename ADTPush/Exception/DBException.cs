using ADTPush.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Exception
{
    public class DBException : System.Exception
    {
        public DBException() { }
        public DBException(System.Exception e)
        {

        }


    }
}
