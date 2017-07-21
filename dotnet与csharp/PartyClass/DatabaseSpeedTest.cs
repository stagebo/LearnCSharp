using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    class DatabaseSpeedTest
    {
        static SqlConnection GetConnection()
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = LoadConfig.GetConnStrings();
            con.Open();
            return con;
        }
        public void Run()
        {
            if (!initDatabase())
            {
                Console.WriteLine("初始化数据库失败！");
                return;
            }
            long t1 = RunInDatabase();
            long t2 = RunInProgram();

            Console.WriteLine($"数据库内部修改耗时：【{t1}】ms");
            Console.WriteLine($"程序修改修改耗时：【{t2}】ms");
            Console.WriteLine($"程序修改比数据库内部修改多：【{t2 - t1}】ms");
        }
        /*数据库运行时间*/
        public long RunInDatabase()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sqlStringBuilder = new StringBuilder();
            sqlStringBuilder.Append(@"update student  set class=c.classname from class c where student.classid=c.id");

            var conn = GetConnection();
            SqlCommand com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = sqlStringBuilder.ToString();

            int result = com.ExecuteNonQuery();

            com.Dispose();
            conn.Close();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        /*程序修改数据运行时间*/
        public long RunInProgram()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlConnection con = GetConnection();
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select  * from [class]", con); //绑定的sql命令
            adapter.Fill(dataSet, "class"); //将数据获取后装载到dataSet中，当作一张表存储在内存中  
            adapter.Dispose();

            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select  * from [student]", con); //绑定的sql命令
            adapter.Fill(dataSet, "student"); //将数据获取后装载到dataSet中，当作一张表存储在内存中      
            adapter.Dispose();

            var dic = dataSet.Tables["class"].Rows.Cast<DataRow>().ToDictionary(r => r["id"], r => r["classname"]);
            var dt = dataSet.Tables["student"].Rows.Cast<DataRow>().ToDictionary(r => r["id"],
                r => new object[] { r["id"], r["name"], r["classid"] }); ; ;

            SqlCommand com = new SqlCommand();

            var sql = new StringBuilder("delete from student");

            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = sql.ToString();
            com.ExecuteNonQuery();
            com.Dispose();
            sql.Clear();

            sql.Append(@"  INSERT INTO[student]
           ([id]
           ,[name]
           ,[class]
		   ,[classid])VALUES ");
            bool isStart = true;
            foreach (KeyValuePair<object, object[]> k in dt)
            {
                sql.Append(isStart ? "" : ",");
                isStart = false;
                sql.Append($"({k.Value[0]},'{k.Value[1]}','{dic[k.Value[2]]}',{k.Value[2]})");
            }
            com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = sql.ToString();
            com.ExecuteNonQuery();
            com.Dispose();
            con.Close();
            return sw.ElapsedMilliseconds;
        }

        public bool initDatabase()
        {
            var sql = new StringBuilder(@"
            DROP TABLE[dbo].[class];
            DROP TABLE[dbo].[student];
            CREATE TABLE[dbo].[class](
	            [id] [int] NOT NULL,
                [classname] [varchar](50) NULL
            ) ON[PRIMARY] 
            
            CREATE TABLE[dbo].[student](
	            [id]   [int]  NOT NULL,
                [name] [varchar](50) NULL,
	            [class] [varchar](50) NULL,
	            [classid] [int] NULL,
             CONSTRAINT[PK_student] PRIMARY KEY CLUSTERED
            (
               [id] ASC
            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
            ) ON[PRIMARY]


            /*删除*/
            if exists ( select * from INFORMATION_SCHEMA.tables
            where table_name = 'class')
            DELETE FROM[dbo].[class];
            if exists ( select * from INFORMATION_SCHEMA.tables
            where table_name = 'student')
            DELETE FROM[dbo].[student];   
            

            /*插入新数据*/
            DECLARE @i int,@count int
            set @i=0
            set @count = 1000
            while @i<@count 
                begin 
                INSERT INTO[dbo].[class]
			               ([id]
			               ,[classname])VALUES(@i,'className_'+convert(varchar(20),@i));
	            INSERT INTO[dbo].[student]
			               ([id]
			               ,[name]
			               ,[class]
			               ,[classid])VALUES(@i,'name_'+convert(varchar(20),@i),'',@i)
	            set @i = @i + 1;
            end
            
            ");
            var conn = GetConnection();
            SqlCommand com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = sql.ToString();
            bool flag = true;
            if (com.ExecuteNonQuery() == -1)
            {
                flag = false;
            }
            com.Dispose();
            conn.Close();
            return flag;
        }
    }
}
#region 数据库表设计
/*************************建表Start********************************************************/

///****** Object:  Table [dbo].[class]    Script Date: 2017/7/21 15:23:23 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//SET ANSI_PADDING ON
//GO

//CREATE TABLE[dbo].[class](
//	[id]
//[int]
//NOT NULL,

//    [classname] [varchar](50) NULL
//) ON[PRIMARY]

//GO

//CREATE TABLE[dbo].[student](
//	[id]
//[int]
//NOT NULL,

//    [name] [varchar](50) NULL,
//	[class] [varchar](50) NULL,
//	[classid]
//[int]
//NULL,
// CONSTRAINT[PK_student] PRIMARY KEY CLUSTERED
//(
//   [id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO

//SET ANSI_PADDING OFF
//GO
/*************************建表End********************************************************/

/************************************插入数据Start******************************************/
///*删除*/
//if exists(select* from INFORMATION_SCHEMA.tables
//where table_name = 'class')
//DELETE FROM[dbo].[class];
//if exists(select* from INFORMATION_SCHEMA.tables
//where table_name = 'student')
//DELETE FROM[dbo].[student];    
//GO
//DECLARE @i int,@count int
//set @i=0
//set @count = 1000
//while @i<@count

//    begin

//    INSERT INTO[dbo].[class]
//			   ([id]
//			   ,[classname])VALUES(@i,'className_'+convert(varchar(20),@i));
//	INSERT INTO[dbo].[student]
//			   ([id]
//			   ,[name]
//			   ,[class]
//			   ,[classid])VALUES(@i,'name_'+convert(varchar(20),@i),'',@i)
//	set @i = @i + 1;
//end
//GO
/************************************插入数据End******************************************/
#endregion