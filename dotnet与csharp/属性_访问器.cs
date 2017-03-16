using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class 属性_访问器
    {
        public static void Run()
        {
            Student stu = new Student();
            stu.Id = 1;
            stu.Name = "wanyongbo";
            int id = stu.Id;
            string name = stu.Name;
        }
    }
    class Student
    {
        private int id;
        private string name;
        public int Id { set { id = value; } get { return id; } }//属性，set和get是访问器
        public string Name { set { name = value; } get { return name; } }//可以对应的设置只读和只写属性，也就是不给set或者get访问器
        public Student() { }
    }
}