using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法
{
    class 动态规划_01背包
    {
        int capaticy;
        int[][] arr;
        /// <summary>
        /// 输入：      
        ///     int[2][] arr 数组第一行表示金子重量，第二行表示金子价值
        ///     int capacity 表示背包容量
        /// 输入：
        ///     能获得的最大价值
        ///     
        /// </summary>
        public void Run()
        {
           
            int s = Solutions(0, capaticy);
            Console.WriteLine("最优解为：{0}", s);
        }
        public int Solution(int n, int c)
        {
            if (n == c) return 0;
            int l = arr[0].Length;
            if (n < arr[0].Length && c - arr[0][n]>=0)
            {
                int giveUp = Solution(n + 1, c - arr[0][n]) + arr[1][n];
                int getIt = Solution(n + 1, c);
                return getIt > giveUp ? getIt : giveUp;
            }
            if(c-arr[0][n]>=0)

                return arr[1][n-1];
            return 0;
        }
        static int c = 12;
        static int[] a = { 3, 3, 5, 4 };
        static int[] b = { 7, 6, 9, 8, 12, 10, 11, 6, 5 };
        static int[] r = new int[a.Length];
        static Dictionary<string, int> l = new Dictionary<string, int>();
        static int times = 0;
        static int referTimes = 0;
        static StringBuilder result = new StringBuilder();

        public static int Solutions(int n, int cc)
        {
            times++;
            string key = n + "," + cc;
            if (l.ContainsKey(key))
            {
                referTimes++;
                return l[key];
            }
            if (n == a.Length)
            {
                l[key] = 0;

                return 0;
            }
            else if (n < c - 1 && cc - a[n] >= 0)
            {
                int get = b[n] + Solutions(n + 1, cc - a[n]);
                int giveUp = Solutions(n + 1, cc);
                if (get > giveUp)
                {
                    l[key] = get;
                    r[n] = 1;
                    return get;
                }
                else
                {
                    r[n] = 0;
                    l[key] = giveUp;
                    return giveUp;
                }
            }
            else if (cc - a[n] >= 0)
            {
                l[key] = b[n];
                r[n] = 1;
                return b[n];
            }
            l[key] = 0;
            r[n] = 0;
            return 0;
        }
    }
}
