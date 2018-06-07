using ADTPush.Infra;
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
        
        public string SelectInfoByPhoneNumber(string phoneNumber)
        {
            conn.Open();


            string selectQueryByPhoneNumber = "SELECT CustomerID FROM KeyMap where PhoneNumber = @phoneNum";

            SqlCommand cmd = new SqlCommand(selectQueryByPhoneNumber, conn);
            cmd.Parameters.AddWithValue("@phoneNum", phoneNumber);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = null;
            while (reader.Read())
            {
                result = String.Format("{0}", reader[0]);
            }

            conn.Close();
            return SelectInfoLogById(result);
        }

        //public void ServerLog(string id, string type, string date, string phone)
        //{
        //    conn.Open();
        //    string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date, ResponseBool) VALUES (@id, @type, @date, @packet, @phoneNum) ";

        //    SqlCommand cmd = new SqlCommand(LogQuery, conn);
        //    cmd.Parameters.AddWithValue("@id", id);
        //    cmd.Parameters.AddWithValue("@type", type);
        //    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
        //    cmd.Parameters.AddWithValue("@packet", "");
        //    cmd.Parameters.AddWithValue("@phoneNum", phone);
        //    cmd.ExecuteReader();
        //    conn.Close();
        //}

        public void ServerLog(string id, string type, string date, string bb, string phone)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date, ResponseBool, PhoneNumber) VALUES (@id, @type, @date, @bool, @phoneNum) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@bool", bb);
            cmd.Parameters.AddWithValue("@phoneNum", phone);
            cmd.ExecuteReader();

            conn.Close();
        }

        public void KeyMapSetting(string id, string phone)
        {
            conn.Open();
            string insertQuery = "IF EXISTS (SELECT PhoneNumber FROM KeyMap Where PhoneNumber = @phoneNum) BEGIN UPDATE KeyMap SET CustomerID = @id, PhoneNumber = @phoneNum Where PhoneNumber = @phoneNum END ELSE BEGIN INSERT INTO KeyMap (CustomerID, PhoneNumber) VALUES (@id, @phoneNum) END";
            //string insertQuery = "INSERT INTO KeyMap (CustomerID, PhoneNumber) VALUES (@id,  @phoneNum) ";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@phoneNum", phone);
            cmd.ExecuteReader();

            conn.Close();
        }
        public void ErrorLog(string id, string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception) VALUES (@id, @msg, @date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            if(id == null)
                cmd.Parameters.AddWithValue("@id", "");
            else
                cmd.Parameters.AddWithValue("@id", id);

            if (msg == null)
                cmd.Parameters.AddWithValue("@Msg", "");
            else
                cmd.Parameters.AddWithValue("@msg", msg);
            
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
        public void ErrorLog(string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception) VALUES (@id, @msg, @date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", "");
            cmd.Parameters.AddWithValue("@msg", msg);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
    }
}
