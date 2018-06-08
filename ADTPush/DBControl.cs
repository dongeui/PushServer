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

        public void RegisterInfo(string id, string token, string phoneNum, string os)
        {
            conn.Open();

            string query = "IF EXISTS (SELECT PhoneNumber FROM Info Where PhoneNumber = @phoneNum) " +
                "BEGIN UPDATE Info SET RegisterToken = @Token, SetDate = @SetDate, CustomerID = @id, OS = @os WHERE PhoneNumber = @PhoneNum " +
                "END ELSE BEGIN INSERT INTO Info (CustomerID, RegisterToken, SetDate, PhoneNumber, OS) VALUES (@id, @token, @SetDate, @phoneNum, @os) END";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@PhoneNum", phoneNum);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@SetDate", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@os", os);
            cmd.ExecuteReader();

            conn.Close();
        }

        public string SelectTokenInfoById(string id)
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
        public string SelectOSTypeById(string id)
        {
            conn.Open();

            string selectQueryById = "SELECT OS FROM Info where CustomerID = @id";

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

            string selectQueryByPhoneNumber = "SELECT CustomerID, OS FROM Info where PhoneNumber = @phoneNum";

            SqlCommand cmd = new SqlCommand(selectQueryByPhoneNumber, conn);
            cmd.Parameters.AddWithValue("@phoneNum", phoneNumber);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = null;
            while (reader.Read())
            {
                result = String.Format("{0}", reader[0]);
            }

            conn.Close();
            return SelectTokenInfoById(result);
        }


        public void ServerLog(string id, string type, string SetDate, string bb, string phoneNum)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ServerLog (CustomerID, Type, Date, ResponseBool, PhoneNumber) VALUES (@id, @type, @Date, @bool, @phoneNum) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@bool", bb);
            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);
            cmd.ExecuteReader();

            conn.Close();
        }



        public void ErrorLog(string id, string msg, string ex, string phoneNum)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception, PhoneNumber) VALUES (@id, @msg, @Date, @ex, @phoneNum) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            if (id == null)
                cmd.Parameters.AddWithValue("@id", "");
            else
                cmd.Parameters.AddWithValue("@id", id);

            if (msg == null)
                cmd.Parameters.AddWithValue("@Msg", "");
            else
                cmd.Parameters.AddWithValue("@msg", msg);

            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);
            cmd.ExecuteReader();

            conn.Close();
        }

        public void ErrorLog(string msg, string ex)
        {
            conn.Open();
            string LogQuery = "INSERT INTO ErrorLog (CustomerID, ErrorMeesage, Date, Exception) VALUES (@id, @msg, @Date, @ex) ";

            SqlCommand cmd = new SqlCommand(LogQuery, conn);
            cmd.Parameters.AddWithValue("@id", "");
            cmd.Parameters.AddWithValue("@msg", msg);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ex", ex);
            cmd.ExecuteReader();

            conn.Close();
        }
    }
}
