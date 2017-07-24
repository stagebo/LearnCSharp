using BaseCSharp.PartyClass;
using BaseCSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet与csharp.PartyClass
{
    class ThreadTest
    {
        public static void Run()
        {
            con = new SqlConnection();
            con.ConnectionString = LoadConfig.GetConnStrings();
            con.Open();
            com = new SqlCommand();
            com.Connection = con;
            com.CommandTimeout = 180;

            RunSingleThread();
            RunMultiThread();
        }
        public static void RunMultiThread()
        {
            com.CommandType = CommandType.Text;
            Random r = new Random();
            char[] str = "abcdefghijklmnopqrstuvwxyz".ToArray();

            int age = r.Next(60);
            string name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
            string sql;
            new Thread(() =>
            {
                sw.Reset();
                sw.Start();
                for (int i = 0; i < totalNum; i++)
                {
                    age = r.Next(60);
                    name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                    sql = "INSERT INTO [Person] ([ID] ,[name] ,[age]) VALUES (" + i + ",'111" + name + "'," + age + ")";
                    com.CommandText = sql;
                    if (com.ExecuteNonQuery() == -1)
                    {
                        Console.WriteLine("defealt");
                    }
                }
                sw.Stop();
                multiTime1 = sw.Elapsed.Seconds;
                Console.WriteLine("multi1Time:"+multiTime1);
            }).Start();

            new Thread(() =>
           {
               sw.Reset();
               sw.Start();
               for (int i = 0; i < totalNum; i++)
               {
                   age = r.Next(60);
                   name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                   sql = "INSERT INTO [Student] ([ID] ,[name] ,[age]) VALUES (" + i + ",'222" + name + "'," + age + ")";
                   com.CommandText = sql;
                   if (com.ExecuteNonQuery() == -1)
                   {
                       Console.WriteLine("defealt");
                   }
               }
               sw.Stop();
               multiTime2 = sw.Elapsed.Seconds;
               Console.WriteLine("multi2Time:"+multiTime2);
           }).Start();

            new Thread(() =>
           {
               sw.Reset();
               sw.Start();
               for (int i = 0; i < totalNum; i++)
               {
                   age = r.Next(60);
                   name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                   sql = "INSERT INTO [User] ([ID] ,[name] ,[age]) VALUES (" + i + ",'333" + name + "'," + age + ")";
                   com.CommandText = sql;
                   if (com.ExecuteNonQuery() == -1)
                   {
                       Console.WriteLine("defealt");
                   }
               }
               sw.Stop();
               multiTime3 = sw.Elapsed.Seconds;
               Console.WriteLine("multi3Time:"+multiTime3);
           }).Start();

        }
        public static void RunSingleThread()
        {

            com.CommandType = CommandType.Text;
            Random r = new Random();
            char[] str = "abcdefghijklmnopqrstuvwxyz".ToArray();
            new Thread(() =>
            {
                sw.Reset();
                sw.Start();
                for (int i = 500; i < totalNum; i++)
                {

                    int age = r.Next(60);
                    string name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                    string sql = "INSERT INTO [Person] ([ID] ,[name] ,[age]) VALUES (" + i + ",'sss" + name + "'," + age + ")";
                    com.CommandText = sql;
                    if (com.ExecuteNonQuery() == -1)
                    {
                        Console.WriteLine("defealt");
                    }

                    age = r.Next(60);
                    name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                    sql = "INSERT INTO [Student] ([ID] ,[name] ,[age]) VALUES (" + i + ",'" + name + "'," + age + ")";
                    com.CommandText = sql;
                    if (com.ExecuteNonQuery() == -1)
                    {
                        Console.WriteLine("defealt");
                    }

                    age = r.Next(60);
                    name = "" + str[r.Next(str.Length)] + str[r.Next(str.Length)] + str[r.Next(str.Length)];
                    sql = "INSERT INTO [User] ([ID] ,[name] ,[age]) VALUES (" + i + ",'" + name + "'," + age + ")";
                    com.CommandText = sql;
                    if (com.ExecuteNonQuery() == -1)
                    {
                        Console.WriteLine("defealt");
                    }

                }
                sw.Stop();
                singleTime = sw.Elapsed.Seconds;
                Console.WriteLine("singleTime:"+singleTime);
            }).Start();

        }
        public static SqlCommand GetCommond()
        {
            if (com == null)
            {
                com = new SqlCommand();
                com.Connection = con;
            }
            return com;
        }
        public static SqlConnection GetConnection()
        {
            if (con == null)
            {
                con = new SqlConnection();
                con.ConnectionString = LoadConfig.GetConnStrings();
                con.Open();
            }
            return con;
        }
        static ThreadTest()
        {
            com = GetCommond();
            con = GetConnection();
        }
        private static SqlCommand com;
        private static SqlConnection con;
        private static int totalNum = 1000;
        private static Stopwatch sw = new Stopwatch();
        private static long singleTime = 0;
        private static long multiTime1 = 0;
        private static long multiTime2 = 0;
        private static long multiTime3 = 0;

    }
}
