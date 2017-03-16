using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法
{
    class 回溯_八皇后问题
    {
        int[] arr;
        int max;
        int solutionCount = 0;
        public void Run(int n)
        {
            max = n;
            arr = new int[n];
            Solution(0);
            Console.WriteLine("解的个数有：{0}",solutionCount);
            Console.ReadKey();
        }
        
        public void Solution(int rank)
        {
            if (rank == max)
            {
                solutionCount++;
                print();
                return;
            }
            for (int i = 0; i < max;i++ )
            {
                arr[rank] = i;
                if (Check(rank))
                {
                    Solution(rank+1);
                }
            }
        }
        public bool Check(int n)
        {
            for (int i = 0; i < n; i++)
            {
                /*检测同列和对角是否有皇后*/
                if (arr[i] == arr[n] || Math.Abs(n - i) == Math.Abs(arr[i] - arr[n]))
                    return false;
            }
            return true;
        }
        public void print()
        {
            Console.WriteLine("------------------------------------------------");
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (j == arr[i])
                    {
                        Console.Write("  *  |");
                    }
                    else
                    {
                        Console.Write("     |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------");
            }

            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i]);
            Console.WriteLine();
            Console.WriteLine("==================================================");
        
        }

    }
  
}
