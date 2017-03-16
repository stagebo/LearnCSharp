using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法
{
    /// <summary>
    /// /*  形如：1/a 的分数称为单位分数。   可以把1分解为若干个互不相同的单位分数之和。 
    /// 例如：
    ///     1 = 1/2 + 1/3 + 1/9 + 1/18 1 = 1/2 + 1/3 + 1/10 + 1/15 
    ///     1 = 1/3 + 1/5 + 1/7 + 1/9 + 1/11 + 1/15 + 1/35 + 1/45 + 1/231 等等，类似这样的分解无穷无尽。   
    /// 我们增加一个约束条件：最大的分母必须不超过30   
    /// 请你求出分解为n项时的所有不同分解法。   
    /// 数据格式要求：   
    /// 输入一个整数n，表示要分解为n项（n<12） 
    /// 输出分解后的单位分数项，中间用一个空格分开。 
    /// 每种分解法占用一行，行间的顺序按照分母从小到大排序。   
    /// 例如， 
    ///     输入： 4  
    ///     程序应该输出：  1/2 1/3 1/8 1/24
    ///                     1/2 1/3 1/9 1/18
    ///                     1/2 1/3 1/10 1/15
    ///                     1/2 1/4 1/5 1/20 
    ///                     1/2 1/4 1/6 1/12 
    /// </summary>
    class 回溯_单位分数
    {

        int max;
        int maxNumber = 30;
        int[] arr;
        HashSet<string> hs = new HashSet<string>();
        public void Run(int max)
        {
            this.max = max;
            arr = new int[max];
            Solution(0);
            Console.ReadKey();
        }
        public void Solution(int n)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
            {
                double t = arr[j];
                if (t != 0)
                    sum += 1 / t;

            }
            if (n >= max && sum != 1)
            {
                return;
            }
            if (n == max && sum == 1)
            {
                print();
                return;
            }

            for (int i = n > 1 ? n : 2; i <= maxNumber; i++)
            {
                arr[n] = i;

                if (n <= max && Check(n))
                {
                    Solution(n + 1);
                }
            }
        }
        public bool Check(int n)
        {
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    double t = arr[j];
                    if (t != 0)
                        sum += 1 / t;

                }
                if (sum > 1 || n > max)
                {
                    return false;
                }
            }
            return true;
        }
        public void print()
        {
            string re = "";
            int[] arrr = arr.ToArray<int>();
            Array.Sort(arrr);
            for (int i = 0; i < arrr.Length; i++)
            {
                re += (arrr[i] + "==");
            }
            if (hs.Contains(re))
                return;
            hs.Add(re);
            /*判断是否有相同的数*/
            for (int i = 0; i < arr.Length; i++)
                for (int j = 0; j < arr.Length; j++)
                    if (arr[i] == arr[j]&&i!=j)
                        return;
            Console.WriteLine(re);
        }
    }
}
