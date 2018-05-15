﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ADTPush
{
    //sql에 접속, 토큰/계약번호 관리
    public class TokenControl
    {

        public string testId = "testId";
        public string testToken = "testToken";
        SqlConnection conn;

        public TokenControl()
        {
            conn = new SqlConnection("Data Source=DESKTOP-DBSBC1F;Initial Catalog=ADTPUSH;Integrated Security=True");
        }

        public void Connection()
        {
            conn.Open();

            //무조건계약번호랑 토큰날라오니 기존에저장되어있던거랑 비교해야함
            //계약번호는 안바뀜, 새로운건 크리에이트해야함

         
            conn.Close();
        }

        public void InsertInfo()
        {
            string insertQuery = "INSERT INTO Info (CustomerID, RegisterToken) VALUES (@id, @token)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@id", testId);
            cmd.Parameters.AddWithValue("@token", testToken);
            cmd.ExecuteNonQuery();
        }
        public void UpdateInfo()
        {

        }
        public void DeleteInfo()
        {

        }
        public void SelectInfo()
        {
            string selectQuery = "SELECT * FROM Info";



        }
        public void SelectInfoById()
        {
            string selectQueryById = "SELECT * FROM Info where CustomerID = @id";
            SqlCommand cmd = new SqlCommand(selectQueryById, conn);
            cmd.Parameters.AddWithValue("@id", testId);

        }
    }
}
