﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp.PartyClass
{
    class DatabaseConn
    {
        public static void Run() {
            //先打开两个类库文件
            SqlConnection con = new SqlConnection();

            // con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
            con.ConnectionString = LoadConfig.GetConnStrings();
            con.Open();

            /*
            SqlDataAdapter 对象。 用于填充DataSet （数据集）。
            SqlDataReader 对象。 从数据库中读取流..
            后面要做增删改查还需要用到 DataSet 对象。
            */

            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "SELECT *  FROM [User]";

            SqlDataReader dr = com.ExecuteReader();//执行SQL语句
            dr.Close();

            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select * from [user]", con); //绑定的sql命令
            adapter.Fill(dataSet, "userinfo"); //将数据获取后装载到dataSet中，当作一张表存储在内存中                

            DataTable dt = dataSet.Tables[0];
            foreach (DataRow drow in dt.Rows)
            {
                Console.WriteLine(drow["id"] + "-" + drow["name"] + "-" + drow["age"] + "");
            }

            con.Close();//关闭数据库
        }
    }
}
