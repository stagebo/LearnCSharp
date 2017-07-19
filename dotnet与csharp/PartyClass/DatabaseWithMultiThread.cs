using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace BaseCSharp.PartyClass
{
    class DatabaseWithMultiThread
    {

        public void Run()
        {
            RunSingleThread();
            RunMultiThread();

        }
        private int allTimes = 3500;
        private long singleTime = 0;
        private long[] times = new long[3];
        private void SetTimes(long time, int index)
        {
            bool isComplete = true;
            if (index == 4)
            {
                singleTime = time;
            }
            else
            {
                times[index] = time;
            }

            for (int i = 0; i < this.times.Length; i++)
            {
                if (times[i] == 0)
                {
                    isComplete = false;
                }
            }
            if (isComplete)
            {
                long multiTime = times[0] + times[1] + times[2];
                Console.WriteLine(string.Format("多线程一共耗时：{0}ms", multiTime));
                Console.WriteLine(string.Format("单线程比多线程耗时多：{0}ms", singleTime - multiTime));
            }
        }
        public void RunSingleThread()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StringBuilder sqlStringBuilder = new StringBuilder(string.Empty);
            sqlStringBuilder.Append($@" delete stu;
                                        declare @i int  
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  

                                        delete stu1;
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu1 (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  

                                        delete stu2;
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu2 (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  
                                        ");

            SqlConnection conn = GetConnection();
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = sqlStringBuilder.ToString();


            int result = com.ExecuteNonQuery();
            com.Dispose();
            conn.Close();
            sw.Stop();
            SetTimes(sw.ElapsedMilliseconds,4);
            Console.WriteLine(string.Format("单线程处理共耗时：{0}ms.", sw.ElapsedMilliseconds));

        }
        /// <summary>
        /// 多线程测试方法，此处不能用for循环带入变量来建立线程，容易导致并发问题以至下标越界*********
        /// </summary>
        public void RunMultiThread()
        {
            Thread th1 = new Thread(new ThreadStart(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                StringBuilder sqlStringBuilder = new StringBuilder(string.Empty);
                sqlStringBuilder.Append($@" 
                                        declare @i int  
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  
                                        ");

                SqlConnection conn = GetConnection();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.Text;
                com.CommandText = sqlStringBuilder.ToString();


                int result = com.ExecuteNonQuery();
                com.Dispose();
                conn.Close();
                sw.Stop();
                SetTimes(sw.ElapsedMilliseconds, 0);
                Console.WriteLine(string.Format("多线程1处理共耗时：{0}ms.", sw.ElapsedMilliseconds));
            }));

            Thread th2 = new Thread(new ThreadStart(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                StringBuilder sqlStringBuilder = new StringBuilder(string.Empty);
                sqlStringBuilder.Append($@" 
                                        declare @i int  
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu1 (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  
                                        ");

                SqlConnection conn = GetConnection();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.Text;
                com.CommandText = sqlStringBuilder.ToString();


                int result = com.ExecuteNonQuery();
                com.Dispose();
                conn.Close();
                sw.Stop();
                SetTimes(sw.ElapsedMilliseconds, 1);
                Console.WriteLine(string.Format("多线程2处理共耗时：{0}ms.", sw.ElapsedMilliseconds));
            }));
            Thread th3 = new Thread(new ThreadStart(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                StringBuilder sqlStringBuilder = new StringBuilder(string.Empty);
                sqlStringBuilder.Append($@" 
                                        declare @i int  
                                        set @i=6   
                                        while @i<{allTimes}  
                                           begin   
                                           insert into stu2 (id,classid,name) values(@i,@i,@i)   
                                           set @i=@i+1   
                                        end  
                                        ");

                SqlConnection conn = GetConnection();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.Text;
                com.CommandText = sqlStringBuilder.ToString();


                int result = com.ExecuteNonQuery();
                com.Dispose();
                conn.Close();
                sw.Stop();
                SetTimes(sw.ElapsedMilliseconds, 2);
                Console.WriteLine(string.Format("多线程3处理共耗时：{0}ms.", sw.ElapsedMilliseconds));
            }));
            th1.Start();
            th2.Start();
            th3.Start();
        }


        static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = LoadConfig.GetConnStrings();
            con.Open();
            return con;
        }
    }
}
