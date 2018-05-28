using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ADTPush.Infra;

namespace ADTPush
{
    //sql에 접속, 토큰/계약번호 관리
    public class DBControl
    {
        public SqlConnection conn;

        public DBControl()
        {
            try
            {
                conn = new SqlConnection("Data Source=DESKTOP-DBSBC1F;Initial Catalog=ADTPUSH;Integrated Security=True");
            }catch(System.Exception e)
            {
                //db 접속이안되서 남기는 에러를 디비에 찍는다?
            }
        }

        public void RegisterInfo(string id, string token)
        {
            conn.Open();

            string query = "IF EXISTS (SELECT CustomerID FROM Info Where CustomerID = @id) BEGIN UPDATE Info SET RegisterToken = @Token, Date = @date WHERE CustomerID = @id END ELSE BEGIN INSERT INTO Info (CustomerID, RegisterToken, Date) VALUES (@id, @token, @date) END";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.ExecuteReader();

            conn.Close();
        }

        public string SelectInfoById(string id)
        {
            conn.Open();

            string selectQueryById = "SELECT RegisterToken FROM Info where CustomerID = @id";

            SqlCommand cmd = new SqlCommand(selectQueryById, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = null;
            while (reader.Read())
            {
                result = String.Format("{0}", reader[0]);
            }
            conn.Close();
            return result;
        }

        public void ServerLog(string id, string type, DateTime date)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date) VALUES (@id, @type, @date) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.ExecuteReader();

            conn.Close();
        }
        
        public void ServerLog(string id, string type, DateTime date, string bb)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date, ResponseBool) VALUES (@id, @type, @date, @bool) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@bool", bb);
            cmd.ExecuteReader();

            conn.Close();
        }
        public void ErrorLog(string id, string date, string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMessage, Date, Excetpion) VALUES (@id, @msg, @date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@msg", msg);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
    }
}
