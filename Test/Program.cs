using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Http;
using System.IO;
using System.Text.RegularExpressions;

namespace Test
{

    class ListNode
    {
        private static String dir = @"F:\HttpTest\";

        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void Write(string fileName, string html)
        {
            try
            {
                FileStream fs = new FileStream(dir + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(html);
                sw.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void Write(string fileName, byte[] html)
        {
            try
            {
                File.WriteAllBytes(dir + fileName, html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
        /// <summary>
        /// 登录博客园
        /// </summary>
        public static void LoginCnblogs()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            String url = "http://passport.cnblogs.com/login.aspx";
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;
            String result = response.Content.ReadAsStringAsync().Result;

            String username = "stagebo";
            String password = "0.123456789dyi";

            do
            {
                String __EVENTVALIDATION = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*?)\"").Match(result).Groups[1].Value;
                String __VIEWSTATE = new Regex("id=\"__VIEWSTATE\" value=\"(.*?)\"").Match(result).Groups[1].Value;
                String LBD_VCID_c_login_logincaptcha = new Regex("id=\"LBD_VCID_c_login_logincaptcha\" value=\"(.*?)\"").Match(result).Groups[1].Value;

                //图片验证码
                url = "http://passport.cnblogs.com" + new Regex("id=\"c_login_logincaptcha_CaptchaImage\" src=\"(.*?)\"").Match(result).Groups[1].Value;
                response = httpClient.GetAsync(new Uri(url)).Result;
                Write("amosli.png", response.Content.ReadAsByteArrayAsync().Result);

                Console.WriteLine("输入图片验证码：");
                String imgCode = "wupve";//验证码写到本地了，需要手动填写
                imgCode = Console.ReadLine();

                //开始登录
                url = "http://passport.cnblogs.com/login.aspx";
                List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                paramList.Add(new KeyValuePair<string, string>("__EVENTTARGET", ""));
                paramList.Add(new KeyValuePair<string, string>("__EVENTARGUMENT", ""));
                paramList.Add(new KeyValuePair<string, string>("__VIEWSTATE", __VIEWSTATE));
                paramList.Add(new KeyValuePair<string, string>("__EVENTVALIDATION", __EVENTVALIDATION));
                paramList.Add(new KeyValuePair<string, string>("tbUserName", username));
                paramList.Add(new KeyValuePair<string, string>("tbPassword", password));
                paramList.Add(new KeyValuePair<string, string>("LBD_VCID_c_login_logincaptcha", LBD_VCID_c_login_logincaptcha));
                paramList.Add(new KeyValuePair<string, string>("LBD_BackWorkaround_c_login_logincaptcha", "1"));
                paramList.Add(new KeyValuePair<string, string>("CaptchaCodeTextBox", imgCode));
                paramList.Add(new KeyValuePair<string, string>("btnLogin", "登  录"));
                paramList.Add(new KeyValuePair<string, string>("txtReturnUrl", "http://home.cnblogs.com/"));
                response = httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(paramList)).Result;
                result = response.Content.ReadAsStringAsync().Result;
                Write("myCnblogs.html", result);
            } while (result.Contains("验证码错误，麻烦您重新输入"));

            Console.WriteLine("登录成功！");

            //用完要记得释放
            httpClient.Dispose();
        }
        static void Main(string[] args) {
            LoginCnblogs();
           // Love.Run();
            Console.ReadKey();
        }
    //    public int val;
    //    public ListNode next = null;

    //    public ListNode(int val)
    //    {
    //        this.val = val;
    //    }
    //    public ListNode(ListNode l)
    //    {
    //        this.val = l.val;
    //    }
    //    public ListNode(int[] arr)
    //    {
    //        this.val = arr[0];
    //        ListNode l = this;
    //        for (int i = 1; i < arr.Length; i++)
    //        {
    //            l.next = new ListNode(arr[i]);
    //            l = l.next;
    //        }
    //    }
    //}
    //class T
    //{
    //    public int data;
    //    public T t;
    //    public T(int data)
    //    {
    //        t = null;
    //        this.data = data;
    //    }
    //}
    //public class HelloJob : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        Console.WriteLine("作业执行，jobSays:" + context.JobDetail.JobDataMap.GetString("helloJob"));
    //    }
    //}
    //public class DumbJob : IJob
    //{
    //    /// <summary>
    //    ///  context 可以获取当前Job的各种状态。
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public void Execute(IJobExecutionContext context)
    //    {

    //        JobDataMap dataMap = context.JobDetail.JobDataMap;

    //        string content = dataMap.GetString("jobSays");

    //        Console.WriteLine("作业执行，jobSays:" + content);
           
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //从工厂中获取一个调度器实例化
    //        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

    //        scheduler.Start();       //开启调度器

    //        //==========例子1（简单使用）===========

    //        IJobDetail job1 = JobBuilder.Create<HelloJob>()  //创建一个作业
    //            .WithIdentity("作业名称", "作业组")
    //            .UsingJobData("helloJob","这里是hellojod")
    //            .Build();

    //        ITrigger trigger1 = TriggerBuilder.Create()
    //                                    .WithIdentity("触发器名称", "触发器组")
    //                                    .StartNow()                        //现在开始
    //                                    .WithSimpleSchedule(x => x         //触发时间，5秒一次。
    //                                        .WithIntervalInSeconds(1)
    //                                        .RepeatForever())              //不间断重复执行
    //                                    .Build();


    //        scheduler.ScheduleJob(job1, trigger1);      //把作业，触发器加入调度器。

    //        //==========例子2 (执行时 作业数据传递，时间表达式使用)===========

    //        IJobDetail job2 = JobBuilder.Create<DumbJob>()
    //                                    .WithIdentity("myJob", "group1")
    //                                    .UsingJobData("jobSays", "Hello World!")
    //                                    .Build();


    //        ITrigger trigger2 = TriggerBuilder.Create()
    //                                    .WithIdentity("mytrigger", "group1")
    //                                    .StartNow()
    //                                    .WithCronSchedule("/1 * * ? * *")    //时间表达式，5秒一次     
    //                                    .Build();


    //        scheduler.ScheduleJob(job2, trigger2);

    //        //scheduler.Shutdown();         //关闭调度器。
    //    }






    //    /**
    //     * 打印竖三角
    //     * *
    //     * * *
    //     * * * *
    //     * * * * *
    //     * * * *
    //     * * *
    //     * *
    //     */
    //    static void printStar()
    //    {
    //        for (int i = 0; i < 20; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                Console.Write(i < 10 ? j <= i ? " |*| " : "-----" : (i + j - 10) <= 8 ? " |*| " : "-----");
    //            }
    //            Console.WriteLine();
    //        }
    //        Console.ReadKey();
    //    }
    //    static void m(T t)
    //    {
    //        t = new T(22);
    //    }
    //    static void methoddd(T t)
    //    {
    //        t.t.t.t = null;//step1
    //        t = null;//step2
    //        t = new T(7);//step3
    //    }
    //    static void printList(ListNode l)
    //    {
    //        while (l != null)
    //        {
    //            Console.WriteLine(l.val);
    //            l = l.next;
    //        }
    //    }
    //    static ListNode Merge(ListNode l1, ListNode l2)
    //    {
    //        ListNode head;

    //        if (l1.val < l2.val)
    //        {
    //            head = new ListNode(l1);
    //            l1 = l1.next;
    //        }
    //        else
    //        {
    //            head = l2;
    //            l2 = l2.next;
    //        }
    //        ListNode t = head;
    //        while (l1 != null && l2 != null)
    //        {
    //            if (l1.val < l2.val)
    //            {
    //                t.next = new ListNode(l1);
    //                t = t.next;
    //                l1 = l1.next;
    //            }
    //            else
    //            {
    //                t.next = new ListNode(l2);
    //                t = t.next;
    //                l2 = l2.next;
    //            }
    //        }
    //        while (l1 != null)
    //        {
    //            t.next = l1;
    //            t = t.next;
    //            l1 = l1.next;
    //        }
    //        while (l2 != null)
    //        {
    //            t.next = l2;
    //            t = t.next;
    //            l2 = l2.next;
    //        }
    //        return head;

    //    }
    //    static int method4()
    //    {
    //        int re = 0;
    //        try
    //        {
    //            Console.WriteLine("try");
    //            return 5;
    //        }
    //        catch
    //        {
    //            Console.WriteLine("try");
    //            re = 4;
    //        }
    //        finally
    //        {
    //            Console.WriteLine("fin");
    //            re = 6;
    //        }
    //        return re;
    //    }
    //    static void method3()
    //    {
    //        //求以下表达式的值，写出您想到的一种或几种实现方法： 1-2+3-4+……+m
    //        for (int xxx = 1; xxx < 100; xxx++)
    //            Console.WriteLine((xxx % 2 == 0) ? (-xxx / 2) : (xxx + 1) / 2);//1 -1 2 -2 3 -3
    //    }
    //    static double method(double x)
    //    {
    //        //起始高度200，最后一次弹起高度x，
    //        //判断是否有x的弹起高度，如果没有，返回-1

    //        int error = -1;//定义错误返回值

    //        double re = x; //定义返回的值，并记最后一次弹起所经历路程

    //        bool flag = true; //定义标记，判断是否有该弹起高度

    //        if (x >= 200 | x <= 0) flag = false;//给的数不合法，没有该弹起高度

    //        while ((x *= 2) < 200 && flag)
    //        {
    //            re += 2 * x;//记中间弹起和落下所经历路程
    //            if (2 * x > 200) flag = false;

    //        }
    //        re += 200;//记最后一次落下路程
    //        if (flag) return re;
    //        return error;
    //    }
    //    public static bool method1(int n)
    //    {
    //        //完数  6=1+2+3
    //        int sum = 0;
    //        for (int i = 1; i < n; i++)
    //            if (n % i == 0) sum += i;

    //        if (sum == n) { Console.WriteLine(n); return true; }
    //        return false;
    //    }
    //}

    //class Stu
    //{
    //    public int id;
    //    public string name;
    //    public Stu(int id, string name)
    //    {
    //        this.id = id;
    //        this.name = name;
    //    }
    }


}
