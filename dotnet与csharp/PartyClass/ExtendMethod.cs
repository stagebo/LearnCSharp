using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    /// <summary>
    /// 扩展方法测试，好用~
    /// 可以改变很多工具类的old develop mode~
    /// 2017年7月21日14:44:58
    /// </summary>
    public static class ExtendMethod
    {
        /// <summary>
        /// 打印列表内容
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="l">泛型集合</param>
        public static void print<T>(this IEnumerable<T> l) {
            Console.WriteLine($"*************print[{l.ToString()}] Start****************");
            foreach (T t in l) {
                Console.WriteLine(t.ToString());
            }
            Console.WriteLine($"*************print[{l.ToString()}] End****************");
        }
        /// <summary>
        /// 打印字符串
        /// </summary>
        /// <param name="str">需要打印的字符串</param>
        public static void print(this string str)
        {
            Console.WriteLine("content:【"+str+"】");
        }
    }
}
