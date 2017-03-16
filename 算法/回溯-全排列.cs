using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 算法
{
    class 回溯_全排列
    {
        static string[] strs;
        //        static List<string> l = new List<string>();
        static HashSet<string> l = new HashSet<string>();
        static string[] arr;
        /// <summary>
        /// 全排列问题可用穷举法实现，事件负责度和回溯法一致，无优点~
        /// 
        /// </summary>
        /// <param name="n"></param>
        public void Run(string str)
        {
            List<string> ll = l.ToList<string>();
            Console.WriteLine("Start!");
            strs = str.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] += i;
            }
            arr = new string[strs.Length];
            Solution(0);
            Console.WriteLine("【"+string.Join("】"+System.Environment.NewLine+"【", l)+"】");
            Console.WriteLine("字符串【{0}】一共有【{1}】种排列~", str, l.Count);

        }
        public void Solution(int n)
        {
            if (n == strs.Length)
            {
                /*去重*/
                l.Add(new Regex("\\d+").Replace(string.Join("", arr), ""));
                return;
            }
            for (int i = 0; i < strs.Length; i++)
            {
                arr[n] = strs[i];
                if (Check(n))
                {
                    Solution(n + 1);
                }
            }
        }
        public bool Check(int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (arr[i] == arr[n] && i != n)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
