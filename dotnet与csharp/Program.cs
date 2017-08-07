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
    partial class Program
    {
        static void Main(string[] args)
        {
            //  new DatabaseHelperTest().Run();
            ConfigurationManager.OpenExeConfiguration("");
            string localVersion = ConfigurationManager.AppSettings["configuration/connectionStrings/add"];//获取本地版本号，TODO
            var v = ConfigurationManager.AppSettings;
            DisplayConnectionStrings();
            Console.ReadKey();
        }
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
