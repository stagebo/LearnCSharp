using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    class 阶乘结果后面的零
    {
        public static void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i + "的阶乘结果：" + C(i));
                Console.WriteLine(i + "的阶乘里面0的个数：" + Cal(i));
            }
        }
        public static  int Cal(int j)
        {
            return j / 5 == 0 ? 0 : j / 5 + Cal(j / 5);
        }
        public static double C(int i)
        {
            if (i == 1) return i;
            return i * C(i - 1);
        }
    }
}
