using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC = System.Console;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BaseCSharp.PartyClass;
using System.Threading;
using System.Windows.Forms;
using dotnet与csharp;
using dotnet与csharp.PartyClass;
using System.Configuration;
using System.Collections;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
/*
* dotnet一般指.Net Framework 框架，是一种平台，一种框架
* c#是一种编程语言，可以开发基于.net平台的应用程序
* 
* 
* .net 可以开发：
*          桌面应用程序  winform
*          Internet应用程序  ASP.NET
*          手机开发  wp8
*          
* Internet的开发模式
*      C/S模式
*      B/S模式
*      
* 
* 
*/
namespace BaseCSharp
{
    class Book
    {
        public Guid id;
        public string name;
        public Book(Guid id, string name)
        {
            this.id = id;
            this.name = name;
            publishTime = DateTime.Now;
        }
        public DateTime publishTime { get; set; }
        public string toJsonString()
        {
            return "12313";
        }
    }
    partial class Program
    {
        static void Main(string[] args)
        {
            string url = "http://stagebo.55555.io/Managerinterface/ExcuteSql?r=stagebo&sql=select*from[t_user]";
            HttpHelper http = new HttpHelper();
            string result = http.SendGet(url);
            Console.WriteLine(result);

            ///Login/Validate
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"uid","c" },
                {"pwd","c" }
            };
            url = "http://stagebo.55555.io/Login/Validate";
            result = http.SendPost(url,param);
            Console.WriteLine(result);

            //Console.WriteLine(string.Format("{0:N0}", 123.456));
            //try
            //{
            //    string postString = "uid=test&pwd=test";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            //    byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            //    string url = "http://stagebo.55555.io/Managerinterface/ExcuteSql?r=stagebo&sql=select*from[t_users]";//地址  
            //    WebClient webClient = new WebClient();
            //    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            //    byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            //    string srcString = Encoding.UTF8.GetString(responseData);//解码  
            //    string resulst = HttpWebReuqestGet(url,new CookieContainer());
            //}
            //catch(Exception ex)
            //{
            //}
            ////string url="";
            ////string reget = HttpWebReuqestGet("Http://stagebo.55555.io", null);
            //string result = HttpWebReuqestPost("http://stagebo.55555.io/Login/Validate",
            //    new Dictionary<string, string>()
            //    {
            //        { "uid","c"},
            //        { "pwd","c"}
            //    }, null);
            Console.ReadKey();
        }
        #region HttpWebReuqest访问

        /// <summary>
        /// HttpWebReuqest访问web资源，GET方式
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="cookieContainer">Cookie容器</param>
        /// <returns>web资源string</returns>
        public static string HttpWebReuqestGet(string url, CookieContainer cookieContainer)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                httpWebRequest.Accept = "*/*";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = 5000;
                httpWebRequest.CookieContainer = cookieContainer;
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();


                Stream respStream = httpWebResponse.GetResponseStream();
                ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以   
                //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）   
                StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
                string res = httpWebResponse.ContentLength + "";
                string strBuff = respStreamReader.ReadLine();// ReadToEnd();

                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string webString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebResponse.Close();
                cookieContainer.Add(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri));
                return webString;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        /// <summary>
        /// HttpWebReuqest访问web资源，POST方式
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="parameterDictionary">参数集合</param>
        /// <param name="cookieContainer">Cookie容器</param>
        /// <returns>web资源string</returns>
        public static string HttpWebReuqestPost(string url, Dictionary<string, string> parameterDictionary, CookieContainer cookieContainer)
        {
            StringBuilder parameterStringBuilder = new StringBuilder(string.Empty);
            bool isFirstParameter = true;
            if (parameterDictionary != null)
            {
                foreach (KeyValuePair<string, string> item in parameterDictionary)
                {
                    parameterStringBuilder
                        .Append(!isFirstParameter ? "&" : "")
                        .Append(item.Key)
                        .Append("=")
                        .Append(item.Value);
                    isFirstParameter = false;
                }
            }
            string parameterString = parameterStringBuilder.ToString();
            try
            {
                byte[] parameterByteArray = Encoding.ASCII.GetBytes(parameterString);
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Accept = "*/*";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 5000 * 2;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                httpWebRequest.ContentLength = parameterByteArray.Length;
                httpWebRequest.CookieContainer = cookieContainer;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(parameterByteArray, 0, parameterByteArray.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string webString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebResponse.Close();
                cookieContainer.Add(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri));
                return webString;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        #endregion
        // Show how to use ConnectionStrings.
        static void DisplayConnectionStrings()
        {
            // Get the ConnectionStrings collection.
            ConnectionStringSettingsCollection connections =
            ConfigurationManager.ConnectionStrings;
            Console.WriteLine();
            Console.WriteLine("Connection strings:");
            // Loop to get the collection elements.
            IEnumerator conEnum =
            connections.GetEnumerator();
            int i = 0;
            while (conEnum.MoveNext())
            {
                string name = connections[i].Name;
                string connectionString = connections[name].ConnectionString;
                string provider = connections[name].ProviderName;
                Console.WriteLine("Name:               {0}", name);
                Console.WriteLine("Connection string:  {0}", connectionString);
                Console.WriteLine("Provider:           {0}", provider);
            }
        }

    }

}
