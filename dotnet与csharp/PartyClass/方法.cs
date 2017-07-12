using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    class 方法
    {

        /*
         * 方法的包括修饰符，返回类型，方法名，参数以及方法体
         * var关键字的使用：
         *          var关键字只能使用于本地变量，只能在变量声明中包含了初始化，也就是说从右边可以推断出左边的数据类型。一旦编译器推断出他的数据类型，就不能再改变。
         *       
         * 方法的参数：
            。。性参与实参
            。。值参数
            。。输出参数
            。。引用参数
         * 
         * 1、使用ref型参数时，传入的参数必须先被初始化。对out而言，必须在方法中对其完成初始化。

            2、使用ref和out时，在方法的参数和执行方法时，都要加Ref或Out关键字。以满足匹配。

            3、out适合用在需要retrun多个返回值的地方，而ref则用在需要被调用的方法修改调用者的引用的时候。

         */
        static void Run()
        {
            int c, d = 0;
            int[] arr = { 1, 1, 0 };
            plus1(1, 1, ref d);
            plus2(1, 1, out c);
            Console.WriteLine("1+1=" + plus(1, 1));
            Console.WriteLine("ref1+1=" + d);
            Console.WriteLine("out1+1=" + c);
            Console.WriteLine("参数数组1+1=" + 方法.plus3(arr));
            Console.WriteLine("可变参数1+1=" + 方法.plus3(1, 1, 1));
            Console.WriteLine("可选参数默认1+1=" + plus4());
            Console.WriteLine("可选参数1+1=" + plus4(1, 1));


            Console.ReadKey();

        }
        static int plus(int a, int b) { return a + b; }
        static void plus1(int a, int b, ref int c) { c = a + b; }
        static void plus2(int a, int b, out int c) { c = a + b; }

        //参数数组
        static int plus3(params int[] arr)
        {
            arr[2] = arr[0] + arr[1];
            return arr[2];
        }
        //可选参数
        static int plus4(int a = 1, int b = 1)
        {
            return a + b;
        }
    }
}
