using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTPush.Infra
{
    public class Packet
    {
        //이전임님이랑 정해서 + 앱에서받을것들
        public const int HeaderSize = 4;
        public string CustomerID { get; set; }
        public string Token { get; set; }
        public string State { get; set; }
        public Packet() { }

        //모듈로부터 계약번호만 받을때
        public Packet(string id)
        {
            CustomerID = id;
        }
        //폰으로부터 계약번호, 토큰 받을때
        public Packet(string id, string token)
        {
            CustomerID = id;
            Token = token;
        }
    }
}
