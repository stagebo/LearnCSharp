using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    class 新特性
    {
        public static void Run()
        {
            string name = "wyb";
            int age = 23;
            string info = string.Format("my name is {0},i am {1} years old.",name,age);
            Console.WriteLine(info);

           string info1=$"my name is {name},i am {age} years old.";
            //Console.WriteLine(info1);
            NewAttributeTest newAT=new NewAttributeTest();
            //string str=newAT?.id;
        }
       
    }
        class NewAttributeTest
        {
            public int id;
            public int getID(){return id;}
        }
    
}
