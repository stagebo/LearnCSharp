using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class 索引
    {
        /*
         * 什么是索引：索引是一组get和set访问器，类似于属性的访问器。
         * 索引的重载。必须有不同的参数列表。
         */

        public static void Run()
        {
            Student s = new Student();
            s[0] = 123;
            Console.WriteLine(s[0]);
            Console.ReadKey();
        }
        class Student
        {
            private int id;
            //private string name = "ddd  ";
            private int grade;
            private int height;
            //private double weight;
            private int age;



            public Student() { }
            public int this[int index]//索引的声明和实现。
            {
                get
                {
                    switch (index)
                    {
                        case 0: return id;
                        case 1: return grade;
                        case 2: return height;
                        case 3: return age;
                        default: return -1;
                    }
                }
                set
                {
                    switch (index)
                    {
                        case 0: id = value; break;
                        case 1: grade = value; break;
                        case 2: height = value; break;
                        case 3: age = value; break;
                        default: break;

                    }
                }
            }

        }
    }
}
