using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace BaseCSharp.PartyClass
{
    /// <summary>
    /// c#6.0新特性试验
    /// </summary>

    class NewCSharp
    {
        /// <summary>
        /// 入口
        /// </summary>
        public static void Run()
        {
            string name = "wyb";
            int age = 23;
            string t2 = $"{name}_{age}";//string.Format改进
            WriteLine(t2);

            Person p = null;
            string names = p == null ? "" : p.name;//空值条件判断
            WriteLine(names);
            string names1 = p?.name;
            WriteLine(names1);

            WriteLine(Add(23, 45));
            WriteLine(SumAdd(100));

            WriteLine(nameof(NewCSharp));//获取类名

            

            
        }
        /// <summary>
        /// 简单加法实现~
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int Add(int a, int b) => a + b;//lambda表达式灵活使用
        /// <summary>
        /// 累加函数实现
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int SumAdd(int n) => n == 0 ? 0 : n + SumAdd(n - 1);
    }
}
