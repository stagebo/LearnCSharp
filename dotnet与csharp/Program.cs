using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC = System.Console;
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
namespace dotnet与csharp
{
   partial class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("11111111111111"); 
            //new Lambda表达式().Run();
            //dotnet与csharp.PartyClass.ReflectTest.Run();
            Console.ReadKey();
        }
    }
    interface A { }
    class B { }
    class C : B, A { }
}
