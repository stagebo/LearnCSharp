using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数学问题
{
    class 递归
    {
        static void Run(string[] args)
        {
            Console.WriteLine("请输入阶乘数字：");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("阶乘的结果是{0}，如果结果是-1，表示输入的是负数！", Factorial(num));
            Console.ReadKey();

        }
        //用递归算阶乘
        static int Factorial(int n)
        {

            if (n < 0) return -1;
            if (n == 0) return 1;
            if (n == 1) return 1;
            return n * Factorial(n - 1);

        }
    }
}
