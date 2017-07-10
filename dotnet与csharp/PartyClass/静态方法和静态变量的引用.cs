using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    public class 静态方法和静态变量的引用
    {
        public static void Run()
        {
            Tools.Intro(Tools.name);
            Console.ReadKey();
        }
    }
    //类的静态成员常常用作工具类，比如字体设置，sql链接，等等。不用new对象，直接用类名来调用，方便快捷


    class Tools
    {
        static public string name = "wanyongbo";//静态成员变量
        static public void Intro(string names)//静态方法
        {
            Console.WriteLine("静态方法：我是" + names);
        }

    }
}
