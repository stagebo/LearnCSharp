using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class Lambda表达式
    {
        public delegate void Del(int i);
        public void Run()
        {
            string[] nameList = { "一","二","三","四","五","六","七"};
            string[] firstNameList = { "张", "李", "王" };
            List<Person> l = new List<Person>();
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            { 
                string name=firstNameList[r.Next(firstNameList.Length)]+
                    nameList[r.Next(nameList.Length)];
                int age = 20 + r.Next(10);
                l.Add(new Person(name,age));
            }
            Console.WriteLine("原始名单："+l.Count);
            foreach (Person p in l)
            {
                Console.WriteLine("姓名：{0}，年龄：{1}",p.name,p.age);
            }
            /*lambda表达式*/
            IEnumerable<Person> l2 = l.Where<Person>(p=>p.age>25&p.name.StartsWith("张"));
            
            
            Console.WriteLine("筛选名单："+l2.Count<Person>().ToString());
            foreach (Person p in l2)
            {
                Console.WriteLine("姓名：{0}，年龄：{1}", p.name, p.age);
            }
        }
        
    }
    class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
    }
}
