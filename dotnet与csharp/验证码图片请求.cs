using System;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;

namespace dotnet与csharp
{
    class 验证码图片请求
    {
        public static void Run()
        {
            for (int i = 0; i < 2000; i++)
            {                
                /*{"hash1":444,"hash2":444,"url":"/site/captcha?v=5917c920f409b"}*/
                string info = GetInfo("http://202.113.4.11:8800/site/captcha?refresh=1");
                string fileName = "D:\\VS\\测试图片\\" + Guid.NewGuid();
                string r = info.Split(':')[3].Replace("\"", "").Replace("}", "");
                GetFile("http://202.113.4.11:8800/" + r + "", fileName);
                Thread.Sleep(100);
            }

            Console.WriteLine("下载完毕");
        }
        public static void GetFile(string url, string fileName)
        {
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换   
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();

            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容   
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理   
            Stream respStream = httpResp.GetResponseStream();
            int size = 1024 * 50;
            byte[] b = new byte[size];
            long len = httpResp.ContentLength;
            int l = (int)((len - 1) / size + 1);
            FileStream fs = null;
            string file = fileName + ".png";
            try
            {
                fs = new FileStream(file, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            }
            catch
            {
            }
            for (int i = 0; i < l; i++)
            {
                respStream.BeginRead(b, i * size, size, null, null);
                fs.Write(b, 0, b.Length);
            }
            fs.Close();
        }
        public static String SendRequest(String url, Encoding encoding)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream(), encoding);
            return sr.ReadToEnd();
        }
        public static string GetInfo(string url)
        {
            string strBuff = "";
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换   
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容   
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理   
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以   
            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）   
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            strBuff = respStreamReader.ReadToEnd();
            return strBuff;
        }
    }
}
