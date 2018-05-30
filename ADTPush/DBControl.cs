using System;
using System.Data.SqlClient;

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
            }
            catch (System.Exception e)
            {
                //db 접속이안되서 남기는 에러를 디비에 찍는다?
            }
        }

        public void RegisterInfoLog(string id, string token)
        {
            conn.Open();

            string query = "IF EXISTS (SELECT CustomerID FROM InfoLog Where CustomerID = @id) BEGIN UPDATE InfoLog SET RegisterToken = @Token, Date = @date WHERE CustomerID = @id END ELSE BEGIN INSERT INTO InfoLog (CustomerID, RegisterToken, Date) VALUES (@id, @token, @date) END";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.ExecuteReader();

            conn.Close();
        }

        public string SelectInfoLogById(string id)
        {
            conn.Open();

            string selectQueryById = "SELECT RegisterToken FROM InfoLog where CustomerID = @id";

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

        public void ServerLog(string id, string type, string date)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date) VALUES (@id, @type, @date) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.ExecuteReader();

            conn.Close();
        }
        
        public void ServerLog(string id, string type, string date, string bb)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date, ResponseBool) VALUES (@id, @type, @date, @bool) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@bool", bb);
            cmd.ExecuteReader();

            conn.Close();
        }
        public void ErrorLog(string id, string date, string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception) VALUES (@id, @msg, @date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            if(id == null)
                cmd.Parameters.AddWithValue("@id", "NullPacket");
            else
                cmd.Parameters.AddWithValue("@id", id);

            if (msg == null)
                cmd.Parameters.AddWithValue("@Msg", "NullPacket");
            else
                cmd.Parameters.AddWithValue("@msg", msg);

            if (date == null)
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            else
                cmd.Parameters.AddWithValue("@date", date);
           
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
        public void ErrorLog(string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception) VALUES (@id, @msg, @date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", "NULL");
            cmd.Parameters.AddWithValue("@msg", msg);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
    }
}
