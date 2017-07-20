using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数学问题
{
    class 特征值法求数列通项公式验证
    {
        /// <summary>
        /// 定义递推数列：f(n)=2f(n-1)+f(n-2),f(0)=0,f(1)=1.
        /// 通过特征值法求得f(n)=k/4*[(1+k)n方-(1-k)n方]  k=根号2
        /// </summary>
        /// <param name="args"></param>
        public static void Run(string[] args)
        {
            for (int i = 0; i < 50; i++)
                Console.WriteLine(i + "----" + getND(i));

            Console.ReadKey();
        }
        /*用地推公式求值*/
        static int getN(int n)
        {
            double k = Math.Sqrt(2);
            double t1 = Math.Pow((1 + k), n);
            double t2 = Math.Pow((1 - k), n);
            double re = (k / 4 * (t1 - t2));

            return (int)Math.Ceiling(re);
        }
        /*用递推求值*/
        static int getND(int n)
        {
            if (n == 0 || n == 1) return n;

            return getND(n - 1) + getND(n - 2);
        }
        
    }
}
