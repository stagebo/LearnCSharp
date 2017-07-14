using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.PartyClass
{
    /// <summary>
    /// 反射测试类2017.7.7
    /// </summary>
    class ReflectTest1
    {
        public static void Run()
        {
            BaseCSharp.Entity.Person1 p = Reflect("Person") as BaseCSharp.Entity.Person1;
            if (p == null)
            { 
                Console.WriteLine("反射异常13222~");
            }
            else
            {
                p.name = "wyb";
                p.age = 23;
                p.gender = 1;
            }
            Console.WriteLine(p.ToString());
        }
        public static object Reflect(string classname)
        {
            Type type = Type.GetType("BaseCSharp.Entity." + classname);
            if (type == null)
            {
                return null;
            }
            object obj = type.Assembly.CreateInstance("BaseCSharp.Entity." + classname);
            return obj;
        }
    }
}
