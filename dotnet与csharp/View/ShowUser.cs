using BaseCSharp.PartyClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet与csharp
{
    public partial class ShowData : Form
    {
        public ShowData()
        {
            InitializeComponent();
        }
        public static void Run()
        {
            

            Form f = new ShowData();
           // f.Show();
            Application.Run(f);
        }
        private void ShowData_Load(object sender, EventArgs e)
        {

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
            adapter.SelectCommand = new SqlCommand("select top 1000 * from [user] order by id", con); //绑定的sql命令
            adapter.Fill(dataSet, "userinfo"); //将数据获取后装载到dataSet中，当作一张表存储在内存中                

            DataTable dt = dataSet.Tables["userinfo"];
            data_grid.DataSource=dt;
            int index = 1;
            foreach (DataRow drow in dt.Rows)
            {
                //Console.WriteLine(index++ + ":" + drow["id"] + "-" + drow["name"] + "-" + drow["age"] + "");
            }
            //for (int i = 500; i < 100000; i++)
            //{
            //    Random r = new Random();
            //    int age = r.Next(60);
            //    char[] str = "abcdefghijklmnopqrstuvwxyz".ToArray();
            //    string name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
            //    string sql = "INSERT INTO [User] ([ID] ,[name] ,[age]) VALUES ("+i+",'"+name+"',"+age+")";
            //    adapter.SelectCommand= new SqlCommand(sql, con);
            //    adapter.Fill(dataSet,"insert");
            //}
            con.Close();//关闭数据库
        }
    }
    #region  数据库表脚本文件
    //USE [TDQS_DWFX]
    //GO

    ///****** Object:  Table [dbo].[User]    Script Date: 2017/7/12 16:08:19 ******/
    ///******                          Author:wyb                            ******/
    //SET ANSI_NULLS ON
    //GO

    //SET QUOTED_IDENTIFIER ON
    //GO

    //SET ANSI_PADDING ON
    //GO

    //CREATE TABLE [dbo].[User](
    //    [ID] [int] NOT NULL,
    //    [name] [varchar](50) NULL,
    //    [age] [int] NULL
    //) ON [PRIMARY]

    //GO

    //SET ANSI_PADDING OFF
    //GO


    #endregion
}