using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafePointer
{

    unsafe class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            double n = 5;
            for (int i = 0; i < 100; i++)

                Console.WriteLine(i+"---"+Cube(i));

            Console.ReadKey();
        }
        static double Cube(double n)
        {
            double a = n;
            double x = a / 2;
            for (int i = 0; i < 10; i++)
            {
                x = 2 * x / 3 + a / 3 / x / x;
            }
                return x;
        }
        static float QSqrt(float n)
        {
            long i;
            float x, y;
            const float f = 1.5F;

            x = n * 0.5F;
            y = n;
            i = *(long*)&y;
            i = 0x5f3759df - (i >> 1);
            y = *(float*)&i;
            y = y * (f - x * y * y);
            y = y * (f - x * y * y);
            y = y * (f - x * y * y);
            return 1 / y;
        }
        static double Q(double n)
        {
            double i, x, y,f=1.5;
            x = 0.5 * n;
            y = n;
            i = y;
            i = 0x5f3759df - ((int)n >> 1);
            y = *(float*)&i;
            y = y * (f - x * y * y);
            y = y * (f - x * y * y);
            y = y * (f - x * y * y);
            return 1 / y;
        }
        static double NewTon(double n)
        {
            double x1 = n / 2;
            double x2 = (x1 + n / x1) / 2;
            for (int i = 0; i < 50; i++)
            {
                x1 = x2;
                x2 = (x1 + n / x1) / 2;
            }
            return x2 ;
        }
        static double New(double n,double a)
        {
            Console.WriteLine(n);
            return 0.5*(n+a/n);
        }
    }
    class Stu
    {
        public int x;
    }
    struct Point
    {
        public double x;
        public double y;
    }
}
