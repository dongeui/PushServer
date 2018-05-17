﻿using System;
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
            conn = new SqlConnection("Data Source=DESKTOP-DBSBC1F;Initial Catalog=ADTPUSH;Integrated Security=True");
        }

       /// <summary> 
       /// CustomerId, Token Register & Update
       /// </summary>
        public void RegisterInfo(string id, string token)
        {
            conn.Open();
            string query = "IF EXISTS (SELECT CustomerID FROM Info Where CustomerID = @id) BEGIN UPDATE Info SET RegisterToken = @Token WHERE CustomerID = @id END ELSE BEGIN INSERT INTO Info (CustomerID, RegisterToken, Date) VALUES (@id, @token, @date) END";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            var data = cmd.ExecuteReader();
            while (data.Read())
            {
                Console.WriteLine(String.Format("{0}", data[0]));
            }
            conn.Close();
        }
     
        //public bool CheckInfo(string id, string token)
        //{
        //    bool result = false;
        //    string select = "SELECT CustomerID FROM Info where CustomerID = @id";
        //    SqlCommand selectCmd = new SqlCommand(select, conn);
        //    selectCmd.Parameters.AddWithValue("@id", id);

        //    if (selectCmd.ExecuteNonQuery() != 0)
        //    {
        //        string check = "SELECT * FROM Info where CustomerID = @id, RegisterToken = @token";
        //        SqlCommand checkCmd = new SqlCommand(check, conn);
        //        checkCmd.Parameters.AddWithValue("@id", id);
        //        checkCmd.Parameters.AddWithValue("@id", token);
        //        if(checkCmd.ExecuteNonQuery() != 0)
        //        {
        //            //id, token 매핑됐으니
        //            result = true;
        //        }
        //        else
        //        {
        //            //토큰업데이트 후 재귀돌려서 true리턴해줄수있게
        //            var res = UpdateInfo(id, token);
        //            if(res) CheckInfo(id, token);
        //            //Update실패했으면 로그
        //        }
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    return result;
        //}


        public string SelectInfoById(string id)
        {
            conn.Open();
            string selectQueryById = "SELECT * FROM Info where CustomerID = @id";
            SqlCommand cmd = new SqlCommand(selectQueryById, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = "";
            while (reader.Read())
            {
                result = String.Format("{0}", reader[0]);
            }
            conn.Close();
            return result;
        }
    }
}
