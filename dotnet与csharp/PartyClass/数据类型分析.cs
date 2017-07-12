using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    class 数据类型分析
    {
        /// <summary>
        /// 1、数据类型分为值类型与引用类型，值类型存在栈中，引用类型内容存在堆中，引用存在栈中
        /// 2、引用数据类型的标识符是new，表在堆中开辟数据空间，所谓引用，也就是指针
        /// 3、string是不可变引用对象，所以包含了引用类型的特性，但作为参数传递并不会改变本身的值
        ///     结论：string是引用数据类型，编译器对器做了特殊处理，可能是字符串池
        /// 4、鉴于以上string的特性，所以在完成字符串拼接的时候一般用StringBuilder而不是直接用string，因为string每更改一次内容就
        ///     抛弃原来的内容重新申请内存空间，涉及到太多的处理过程，而StringBuilder则避免了此问题，提高效率优化运行速度~~~
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Run()
        {
            Console.WriteLine(DateTime.Now.AddHours(-1));
            double d = 0;
            for (int i = 1; i < 6; i++)
            {
                d += Math.Pow(36, i);
            }
            bool? flag = null;
            if (flag == false)
                Console.WriteLine("1312");
            S s = new S();
            Console.WriteLine(s.ToString());
            Console.WriteLine(s);

            object[] aa = new object[10];
            Console.WriteLine(aa[0] + "--");

            int[] a = new int[10];
            Console.WriteLine(a[0]);
            Console.ReadKey();

        }
        static void Change(StringBuilder s)
        {
            s.Append("222");
        }
    }
    class S
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
